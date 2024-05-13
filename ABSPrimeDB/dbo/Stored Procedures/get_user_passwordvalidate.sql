CREATE PROCEDURE [dbo].[get_user_passwordvalidate]
	@UserId INT,
	@Password varchar(50)
AS
DECLARE @HashedPassword varbinary(64)
DECLARE @NewHashedPassword varbinary(64)
DECLARE @Salt varchar(36)
DECLARE @PasswordsMatch bit

	SELECT 
		@HashedPassword = [Password]
		,@Salt = Salt
	FROM 
		users
	WHERE
		UserId = @UserId

	SET @NewHashedPassword = HASHBYTES('SHA2_256', CONCAT(@Password, @Salt))
	
	IF(@NewHashedPassword = @HashedPassword)
	BEGIN
		SET @PasswordsMatch = 1;
	END
	ELSE
	BEGIN
		SET @PasswordsMatch = 0;
	END
	
	SELECT @PasswordsMatch as PasswordsMatch

