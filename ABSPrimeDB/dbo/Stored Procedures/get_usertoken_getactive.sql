CREATE PROCEDURE [dbo].[get_usertoken_getactive]
	@Token VARCHAR(50)
 AS
BEGIN
    SELECT  [user_tokens].[UserId],
            [user_tokens].[Token],
            [user_tokens].[ExpiresAt]
    FROM
        [dbo].[user_tokens]
    WHERE
        [user_tokens].[Token] = @Token
        AND [user_tokens].[ExpiresAt] >= GETDATE();
END;
