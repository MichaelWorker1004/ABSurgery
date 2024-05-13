CREATE PROCEDURE [dbo].[get_case_feedback_byid]
	@Id INT
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
		Id = @Id