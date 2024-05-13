CREATE PROCEDURE [dbo].[insert_user_login_audit]
    @UserId INT,
    @EmailAddress varchar(320),
    @ApplicationId INT,
    @LoginIpAddress VARCHAR(50),
    @LoginUserAgent VARCHAR(500),
    @LoginSuccess BIT,
    @LoginFailureReason VARCHAR(500),
    @CreatedByUserId INT,
    @LastUpdatedByUserId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[user_login_audit]
        ([UserId], [EmailAddress], [ApplicationId], [LoginIpAddress], [LoginUserAgent], [LoginSuccess], [LoginFailureReason], [CreatedByUserId], [LastUpdatedByUserId])
    VALUES
        (@UserId, @EmailAddress, @ApplicationId, @LoginIpAddress, @LoginUserAgent, @LoginSuccess, @LoginFailureReason, @CreatedByUserId, @LastUpdatedByUserId);

END
