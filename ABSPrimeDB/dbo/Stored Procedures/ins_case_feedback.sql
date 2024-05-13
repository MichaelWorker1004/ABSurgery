CREATE PROCEDURE [dbo].[ins_case_feedback]
    @UserId int,
    @CaseHeaderId int,
    @Feedback varchar(5000),
    @CreatedByUserId int
AS
    INSERT INTO user_case_feedback (UserId,CaseHeaderId, Feedback, CreatedByUserId,LastUpdatedByUserId)
    VALUES (@UserId, @CaseHeaderId, @Feedback, @CreatedByUserId,@CreatedByUserId)

    DECLARE @Id INT
    SET @Id = @@IDENTITY
    EXEC [dbo].[get_case_feedback_byid] @Id

