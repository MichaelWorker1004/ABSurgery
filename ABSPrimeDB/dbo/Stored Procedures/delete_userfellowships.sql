CREATE PROCEDURE [dbo].[delete_userfellowships]
	@Id INT
AS
	DELETE FROM user_fellowships
	WHERE Id = @Id

RETURN 0
