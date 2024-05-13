CREATE PROCEDURE [dbo].[reset_passwork_using_guid]
    @ResetGUID [uniqueidentifier],
    @NewPassword [varchar](64)
AS
    DECLARE @UserId [int]
    SET @UserId = (SELECT [UserId] FROM [reset_guids] WHERE [ResetGUID] = @ResetGUID)
    DECLARE @ExpireDate [datetime]
    SET @ExpireDate = (SELECT [ExpireDate] FROM [reset_guids] WHERE [ResetGUID] = @ResetGUID)
    DECLARE @CurrentDate [datetime]
    SET @CurrentDate = GETUTCDATE()
    IF (@CurrentDate <= @ExpireDate AND @UserId IS NOT NULL)
    BEGIN
        DECLARE @salt VARCHAR(36) = CONVERT(VARCHAR(36), NEWID())
        DECLARE @hashedpassword varbinary(64)
        SET @hashedpassword = HASHBYTES('SHA2_256', CONCAT(@NewPassword, @salt))
        UPDATE [Users]
        SET [Password] = @hashedpassword,
            [Salt] = @salt,
            [PasswordLastModified] = @CurrentDate,
            [ResetRequired] = 0
        WHERE [UserId] = @UserId
        DELETE FROM [reset_guids]
        WHERE [ResetGUID] = @ResetGUID
        SELECT 1 AS [Result]
    END
    ELSE
    BEGIN
        SELECT 0 AS [Result]
    END