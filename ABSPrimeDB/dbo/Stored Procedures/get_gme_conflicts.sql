CREATE PROCEDURE [dbo].[get_gme_conflicts]
    @UserId INT
AS
    SELECT
    dateadd(day,1,G1.EndDate) AS StartDate,
    dateadd(day,-1,G2.StartDate) AS EndDate,
    G1.Id AS PreviousRotationId,
    G2.Id AS NextRotationId
FROM
    dbo.gme_rotations AS G1
LEFT JOIN
    dbo.gme_rotations AS G2 ON G1.UserId = G2.UserId
WHERE
    DATEADD(day,1,G1.EndDate)<G2.StartDate AND G2.StartDate= (SELECT MIN(gme_rotations.StartDate) FROM dbo.gme_rotations WHERE gme_rotations.UserId=@UserId AND gme_rotations.StartDate>G1.StartDate)
    AND G1.UserId=@UserId