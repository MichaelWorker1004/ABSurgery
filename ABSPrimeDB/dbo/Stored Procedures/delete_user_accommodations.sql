CREATE PROCEDURE [dbo].[delete_user_accommodations]
	@Id INT
AS
	DELETE FROM User_Accommodations
	WHERE Id = @Id

RETURN 0
