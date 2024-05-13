CREATE PROCEDURE [dbo].[update_case_feedback]
	@Id INT,
	@CaseHeaderId INT,
	@Feedback VARCHAR(5000),
	@LastUpdatedByUserId INT
AS
	UPDATE user_case_feedback
	SET
		CaseHeaderId = @CaseHeaderId,
		Feedback = @Feedback,
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @Id

	EXEC [dbo].[get_case_feedback_byid] @Id