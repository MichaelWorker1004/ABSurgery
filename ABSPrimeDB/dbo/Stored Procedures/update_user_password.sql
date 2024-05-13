CREATE PROCEDURE [dbo].[update_user_password]
    @UserId INT,
    @OldPassword VARCHAR(50),
    @NewPassword VARCHAR(50)
AS
    DECLARE @PasswordReset BIT = 0
    DECLARE @OldHashedPassword varbinary(64) = (SELECT  Password FROM users WHERE UserId = @UserId)
    DECLARE @OldSalt varchar(36) =  (SELECT SALT FROM users WHERE UserId = @UserId)
    IF @OldHashedPassword= HASHBYTES('SHA2_256', CONCAT(@OldPassword, @OldSalt)) BEGIN
        DECLARE @salt VARCHAR(36) = CONVERT(VARCHAR(36), NEWID())
        declare @hashedpassword varbinary(64)
        select @hashedpassword = HASHBYTES('SHA2_256', CONCAT(@NewPassword, @salt))
        update users set salt = @salt, Password = @hashedpassword,ResetRequired=0 where UserId = @UserId
        SET @PasswordReset = 1
        SELECT @PasswordReset AS PasswordReset
        END
    ELSE BEGIN
        SELECT @PasswordReset AS PasswordReset
    END
