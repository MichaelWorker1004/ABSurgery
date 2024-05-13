CREATE PROCEDURE [dbo].[ins_reset_guid]
    @UserName [varchar](100)
AS
    DECLARE @Email [varchar](100)
    SET @Email = (SELECT [EmailAddress] FROM [Users] WHERE [UserName] = @UserName)
    DECLARE @UserId [int]
    SET @UserId = (SELECT [UserId] FROM [Users] WHERE [UserName] = @UserName)
    DECLARE @ResetGUID [uniqueidentifier]
    SET @ResetGUID = NEWID()
    DECLARE @ExpireDate [datetime]
    SET @ExpireDate = DATEADD(MINUTE, 30, GETDATE())
    DECLARE @FirstName [varchar](50)
    SET @FirstName = (SELECT [FirstName] FROM [user_profiles] WHERE [UserId] = @UserId)
    DECLARE @LastName [varchar](50)
    SET @LastName = (SELECT [LastName] FROM [user_profiles] WHERE [UserId] = @UserId)
    IF @Email IS NOT NULL AND @UserId IS NOT NULL
    BEGIN
    IF EXISTS (SELECT * FROM [reset_guids] WHERE [UserId] = @UserId)
    BEGIN
        UPDATE [reset_guids]
        SET [ResetGUID] = @ResetGUID,
            [ExpireDate] = @ExpireDate
        WHERE [UserId] = @UserId
        SELECT @ResetGUID AS ResetGUID, @Email AS EmailAddress, @FirstName AS FirstName, @LastName AS LastName
    END
    ELSE
        INSERT INTO [dbo].[reset_guids] ([UserId], [ResetGUID], [ExpireDate])
            VALUES (@UserId, @ResetGUID, @ExpireDate)
        SELECT @ResetGUID AS ResetGUID, @Email AS EmailAddress, @FirstName AS FirstName, @LastName AS LastName
    END
    ELSE
        SELECT NULL AS ResetGUID, NULL AS EmailAddress,NULL AS FirstName, NULL AS LastName
