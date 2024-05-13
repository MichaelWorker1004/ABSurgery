CREATE PROCEDURE [dbo].[get_case_feedback_by_examinerId]
    @ExaminerUserId INT,
    @CaseHeaderId INT
AS
    SELECT
        Id
        ,UserId
        ,CaseHeaderId
        ,feedback
        ,CreatedByUserId
        ,LastUpdatedByUserId
    FROM
        user_case_feedback
    WHERE
        UserId = @ExaminerUserId
        AND CaseHeaderId = @CaseHeaderId