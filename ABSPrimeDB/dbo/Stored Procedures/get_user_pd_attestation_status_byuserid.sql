CREATE PROCEDURE [dbo].[get_user_pd_attestation_status_byuserid]
    @UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'PD_Attestation' Id 
        ,dbo.udf_get_pd_attestation_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId