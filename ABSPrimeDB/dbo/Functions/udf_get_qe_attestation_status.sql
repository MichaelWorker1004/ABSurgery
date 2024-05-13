CREATE FUNCTION [dbo].[udf_get_qe_attestation_status](@UserId INT, @ExamHeaderId INT=0)
RETURNS INT
AS
BEGIN
    --  If all other cards populated + sig_receive,  (other cards handled by UI)
    --  EXCEPT:
    --      licenses can be incomplete
    --      GQ: FES/FLS certificates can be incomplete
    DECLARE @status INT
    SELECT @status =  ISNULL(
        (SELECT 
            CASE WHEN ISNULL(t.sig_receive, '')='' THEN 0
                ELSE 1
            END
        FROM track t
        INNER JOIN exam_specialties es ON es.Code=t.exam
        INNER JOIN exam_types et ON et.Code=t.type
        INNER JOIN Exam_Directory ed ON ed.Id=@ExamHeaderId AND ed.ExamSpecialtyId=es.Id AND ed.ExamTypeId=et.Id AND ed.ExamYear=t.year
        WHERE t.UserId=@UserId
        ), 0)
    
    RETURN @status
END
