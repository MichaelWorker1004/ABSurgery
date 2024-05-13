CREATE PROCEDURE [dbo].[get_user_training_status_byuserid]
    @UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'Training' Id 
        ,dbo.udf_get_training_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId