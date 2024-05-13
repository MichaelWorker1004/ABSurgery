CREATE PROCEDURE [dbo].[get_userlogin_v2]
	@UserName varchar(320),
	@Password varchar(50)
AS
DECLARE @salt VARCHAR(36)
DECLARE @hashedPassword VARBINARY(64)
DECLARE @UserId INT
DECLARE @ApplicationId INT

	SELECT 
		@UserId = UserId
	FROM
		users
	WHERE
		UserName = @UserName and
		[Password] = HASHBYTES('SHA2_256', CONCAT(@Password, Salt));

	EXEC [dbo].[get_loggedin_user_info] @UserId

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

	EXEC [dbo].[get_user_claims] @UserId, @ApplicationId

