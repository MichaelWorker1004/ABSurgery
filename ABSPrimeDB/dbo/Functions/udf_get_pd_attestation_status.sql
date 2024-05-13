CREATE FUNCTION [dbo].[udf_get_pd_attestation_status](@UserId INT, @ExamId INT=0)
RETURNS INT
AS
BEGIN
    --  Only Available if GME is marked complete
    DECLARE @status INT
    DECLARE @ExamCode VARCHAR(8)
    SELECT @ExamCode = ExamCode FROM dbo.Exam_Directory WHERE Id = @ExamId
    DECLARE @ExamSpecialtyId INT
    SELECT @ExamSpecialtyId = ExamSpecialtyId FROM dbo.Exam_Directory WHERE Id = @ExamId
    DECLARE @GmeStatus INT
    SELECT @GmeStatus = dbo.udf_get_gme_status(@UserId, @ExamSpecialtyId)

    SELECT @status=ISNULL((SELECT 
        CASE WHEN @GmeStatus = 0 THEN -1 --GME NOT COMPLETE/PD REF NOT AVAILABLE
            WHEN @GmeStatus = 2 AND AppRefLet.let_sent IS NULL THEN 0 --GME COMPLETE/PD REF AVAILABLE
            WHEN  @GmeStatus = 2  AND AppRefLet.let_sent IS NOT NULL AND AppRefLet.let_rcvd IS NULL THEN 2 --GME COMPLETE/PD REF PENDING
            WHEN  @GmeStatus=2 AND AppRefLet.let_rcvd IS NOT NULL AND AppRefLet.let_approve IS NULL THEN 3 --GME COMPLETE/PD REF SIGNED BUT AWAITING APPROVAL
            WHEN  @GmeStatus=1 and AppRefLet.let_rcvd IS NOT NULL AND AppRefLet.let_approve IS NOT NULL THEN 1 --GME COMPLETE/PD REF APPROVED
        END
    FROM
        dbo.AppRefLet
    WHERE
        userId = @UserId AND
        exam_type = @ExamCode), -1)
RETURN @status
END


