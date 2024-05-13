CREATE PROCEDURE [dbo].[insert_user]
	@EmailAddress varchar(320),
	@Password varchar(50),
	@CreatedByUserId int
AS
	SET NOCOUNT ON
	DECLARE @UserId INT
	DECLARE @Salt varchar(36) = CONVERT(varchar(36), newid())

	INSERT INTO users
	(EmailAddress, [Password], Salt, CreatedByUserId, CreatedAtUtc, LastUpdatedAtUtc, LastUpdatedByUserId)
	VALUES
	(@EmailAddress, HASHBYTES('SHA2_256', CONCAT(@Password, @Salt)), @Salt, @CreatedByUserId, GETUTCDATE(), GETUTCDATE(), @CreatedByUserId);

	SET @UserId = SCOPE_IDENTITY();

	EXEC [dbo].[get_user_info] @UserId

RETURN 0
