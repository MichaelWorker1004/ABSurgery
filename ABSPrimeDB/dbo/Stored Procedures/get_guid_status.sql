CREATE PROCEDURE [dbo].[get_guid_status]
    @ResetGUID [uniqueidentifier]
AS
    DECLARE @ExpireDate [datetime]
    SET @ExpireDate = (SELECT [ExpireDate] FROM [reset_guids] WHERE [ResetGUID] = @ResetGUID)
    DECLARE @CurrentDate [datetime]
    SET @CurrentDate = GETUTCDATE()
    IF (@CurrentDate <= @ExpireDate )
    BEGIN
        SELECT 1 AS Result
    END
    ELSE
    BEGIN
        SELECT 0 AS Result
    END