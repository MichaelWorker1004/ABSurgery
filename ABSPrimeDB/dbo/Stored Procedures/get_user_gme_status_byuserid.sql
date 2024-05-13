CREATE PROCEDURE [dbo].[get_user_gme_status_byuserid]
    @UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'GME' Id 
        ,dbo.udf_get_gme_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId