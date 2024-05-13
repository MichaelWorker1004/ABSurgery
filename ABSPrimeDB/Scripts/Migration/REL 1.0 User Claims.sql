INSERT INTO user_claims (ApplicationClaimId, UserId, CreatedByUserId, LastUpdatedByUserId)
SELECT
    1,
    UserId,
    @UserId,
    @UserId
FROM
    user_profiles
WHERE
    user_profiles.UserId NOT IN (SELECT UserId FROM user_claims WHERE ApplicationClaimId = 1)
UNION
SELECT
    application_claims.ApplicationClaimId,
    user_profiles.UserId,
    @UserId,
    @UserId
FROM
    user_profiles
    INNER JOIN certification_status ON certification_status.CertificationStatusId = user_profiles.CertificationStatusId
    INNER JOIN application_claims ON application_claims.ClaimName = certification_status.StatusGroup
WHERE
    user_profiles.UserId NOT IN (SELECT UserId FROM user_claims WHERE ApplicationClaimId IN (2,3))