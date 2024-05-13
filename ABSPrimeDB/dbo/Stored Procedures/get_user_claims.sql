CREATE PROCEDURE [dbo].[get_user_claims]
	@UserId INT,
    @ApplicationId INT
AS
	SELECT 
     ac.ClaimName
    ,ac.ApplicationId
    ,a.ApplicationName
    ,c.[UserClaimId]
    ,c.[ApplicationClaimId]
    ,c.[UserId]
    ,c.[CreatedByUserId]
    ,c.[CreatedAtUtc]
    ,c.[LastUpdatedAtUtc]
    ,c.[LastUpdatedByUserId]
  FROM 
    [dbo].[user_claims] c
    INNER JOIN [dbo].[application_claims] ac on c.ApplicationClaimId = ac.ApplicationClaimId
    INNER JOIN [dbo].[applications] a on ac.ApplicationId = a.ApplicationId
  WHERE 
    UserId = @UserId
	AND a.ApplicationId = @ApplicationId

RETURN 0
