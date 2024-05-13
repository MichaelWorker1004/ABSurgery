CREATE PROCEDURE [dbo].[get_lapsed_pathway_by_UserId]
    @UserId INT
AS
    SELECT
        IIF(UserId IS NOT NULL,1,0) AS InLapsedPathway,
        ExamType,
        LapsedPathwayTypeId,
        StartDate,
        EndDate,
        FailDate
    FROM
        user_lapsed_pathway
    WHERE
        UserId = @UserId