CREATE PROCEDURE [dbo].[get_user_info]
	@UserId INT,
	@ApplicationId INT
AS
	EXEC [dbo].[get_loggedin_user_info] @UserId

	SELECT 
		uc.UserClaimId,
		uc.ApplicationClaimId,
		ac.ApplicationId,
		ClaimName
	FROM
		user_claims uc inner join 
		application_claims ac on uc.ApplicationClaimId = ac.ApplicationClaimId
	WHERE
		uc.UserId = @UserId
		AND ac.ApplicationId = @ApplicationId

RETURN 0
