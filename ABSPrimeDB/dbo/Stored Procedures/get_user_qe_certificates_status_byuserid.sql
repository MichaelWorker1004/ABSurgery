CREATE PROCEDURE [dbo].[get_user_qe_certificates_status_byuserid]
    @UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'QE_Certificates' Id 
        ,dbo.udf_get_qe_certificates_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId