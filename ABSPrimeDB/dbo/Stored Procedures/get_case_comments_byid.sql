CREATE PROCEDURE [dbo].[get_case_comments_byid]
	@Id INT
AS
	SELECT 
		Id
		,UserId
		,CaseContentId
		,Comments
		,CreatedByUserId
		,LastUpdatedByUserId
	FROM 
		user_case_comments
    WHERE
		Id = @Id