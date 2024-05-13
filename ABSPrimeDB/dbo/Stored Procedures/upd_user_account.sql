CREATE PROCEDURE [dbo].[upd_user_account]
	@UserId int,
	@EmailAddress varchar(320),
	@Password varchar(50),
	@LastUpdatedByUserId int
AS

SET NOCOUNT ON

UPDATE [dbo].[users] SET
	[EmailAddress] = CASE WHEN @EmailAddress IS NOT NULL AND @EmailAddress <> '' THEN @EmailAddress ELSE EmailAddress END,
    [Password] = CASE WHEN @Password IS NOT NULL AND @Password <> '' THEN HASHBYTES('SHA2_256', CONCAT(@Password, Salt)) ELSE Password END,
	[LastUpdatedAtUtc] = GETUTCDATE(),
	[LastUpdatedByUserId] = @LastUpdatedByUserId
WHERE
	[UserId] = @UserId

--endregion
EXEC [dbo].[get_user_account] @UserId

GO