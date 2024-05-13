CREATE PROCEDURE [dbo].[get_user_medical_licenses_status_byuserid]
	@UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'Medical_Licenses' Id
        ,dbo.udf_get_medical_licenses_status(@UserId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId