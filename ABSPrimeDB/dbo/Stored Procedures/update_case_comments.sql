CREATE PROCEDURE [dbo].[update_case_comments]
	@Id INT,
	@CaseContentId INT,
	@Comments VARCHAR(5000),
	@LastUpdatedByUserId INT
AS
	UPDATE user_case_comments
	SET
		CaseContentId = @CaseContentId,
		Comments = @Comments,
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @Id
	
	EXEC [dbo].[get_case_comments_byid] @Id