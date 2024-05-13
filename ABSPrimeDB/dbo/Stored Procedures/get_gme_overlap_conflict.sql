CREATE PROCEDURE [dbo].[get_gme_overlap_conflict]
    @UserId int,
    @StartDate datetime,
    @EndDate datetime,
    @RotationId int = NULL
AS
SELECT IIF(count(Id) > 0, 1, 0) AS OverlapConflict FROM (
    SELECT
        Id
    FROM
        gme_rotations
    WHERE
        UserId = @UserId AND
        ((@startDate BETWEEN StartDate AND EndDate) OR
        (@endDate BETWEEN StartDate AND EndDate) OR
        (@startDate<StartDate and @endDate>startDate)) AND
        IIF(@RotationId IS NULL, 1, IIF(gme_rotations.Id = @RotationId, 0, 1)) = 1
    )Conflict_Ids
RETURN