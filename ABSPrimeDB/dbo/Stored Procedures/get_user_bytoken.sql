CREATE PROCEDURE [dbo].[get_user_bytoken]
	@Token varchar(50)
AS
DECLARE @UserId INT
DECLARE @ApplicationId INT

	SELECT 
		@UserId = u.UserId
	FROM
		dbo.user_tokens ut 
		INNER JOIN dbo.users u on ut.UserId = u.UserId
	WHERE
		Token = @Token

	--TODO - As of March 2023 there is only one application 
	-- using this new User auth table structure (Surgeon Portal)
	-- as new apps are added, we'll have to push the application
	-- name to the application
	SELECT 
		@ApplicationId = ApplicationId
	FROM
		dbo.applications
	where
		ApplicationName = 'Surgeon Portal'

	EXEC [dbo].[get_loggedin_user_info] @UserId

	EXEC [dbo].[get_user_claims] @UserId, @ApplicationId