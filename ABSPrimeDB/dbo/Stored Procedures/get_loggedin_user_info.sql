CREATE PROCEDURE [dbo].[get_loggedin_user_info]
	@UserId INT
AS
    --We need to get the second most recent login date for the user
    --This is because the most recent login date is the current login
	 WITH SecondMostRecentLoginDateCTE AS (
        SELECT 
            UserId, 
            LoginDateTimeUtc AS LastLoginDateUtc
        FROM (
            SELECT 
                UserId, 
                LoginDateTimeUtc,
                ROW_NUMBER() OVER (PARTITION BY UserId ORDER BY LoginDateTimeUtc DESC) AS RowNum
            FROM user_login_audit
        ) AS Subquery
        WHERE RowNum = 2
    )
    SELECT 
        u.UserId,
        DisplayName AS FullName,
        UserName,
        EmailAddress,
        ResetRequired,
        s.LastLoginDateUtc
    FROM 
        users u
        LEFT JOIN user_profiles ON u.UserId = user_profiles.UserId
        LEFT JOIN SecondMostRecentLoginDateCTE s ON u.UserId = s.UserId
    WHERE 
		u.UserId = @UserId
