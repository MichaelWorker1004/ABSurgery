CREATE PROCEDURE [dbo].[get_user_account]
	@UserId INT
AS
	SELECT 
		UserId
		,EmailAddress
		,CreatedByUserId
		,CreatedAtUtc
		,LastUpdatedAtUtc
		,LastUpdatedByUserId
	FROM
		users
	WHERE
		UserID = @UserId
