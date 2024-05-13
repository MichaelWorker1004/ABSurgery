CREATE PROCEDURE [dbo].[update_user]
	@UserId int,
	@EmailAddress varchar(320),
	@Password varchar(50),
	@LastUpdatedByUserId int
AS
	SET NOCOUNT ON

	UPDATE [dbo].[users] SET
		[EmailAddress] = @EmailAddress,
		[Password] = HASHBYTES('SHA2_256', CONCAT(@Password, Salt)),
		[LastUpdatedAtUtc] = GETUTCDATE(),
		[LastUpdatedByUserId] = @LastUpdatedByUserId
	WHERE
		[UserId] = @UserId

RETURN 0
