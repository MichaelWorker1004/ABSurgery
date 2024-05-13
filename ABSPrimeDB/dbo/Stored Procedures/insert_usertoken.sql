CREATE PROCEDURE [dbo].[insert_usertoken]
	@UserId INT,
    @Token VARCHAR(50),
    @ExpiresAt DATETIME2
AS
BEGIN
    INSERT INTO [dbo].[user_tokens] 
    ([UserId], [Token], [ExpiresAt])
    VALUES (@UserId, @Token, @ExpiresAt);

    EXEC [dbo].[get_usertoken_getactive] @Token
END;
