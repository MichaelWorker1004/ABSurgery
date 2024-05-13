CREATE PROCEDURE [dbo].[delete_userdocument_byid]
	@Id INT,
	@UserId INT
AS
	DELETE FROM
		user_documents
	WHERE
		Id = @Id
		AND UserId = @UserId
