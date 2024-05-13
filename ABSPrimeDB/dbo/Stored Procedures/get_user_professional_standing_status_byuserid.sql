CREATE PROCEDURE [dbo].[get_user_professional_standing_status_byuserid]
	@UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'Professional_Standing' Id
        ,dbo.udf_get_prof_standing_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId