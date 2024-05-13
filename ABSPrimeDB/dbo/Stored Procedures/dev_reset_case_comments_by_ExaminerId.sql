CREATE PROCEDURE [dbo].[dev_reset_case_comments_by_ExaminerId]
    @ExaminerUserId int
AS
DELETE FROM user_case_comments WHERE UserId=@ExaminerUserId
