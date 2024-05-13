CREATE PROCEDURE [dbo].[get_gmesummary_byuserid]
	@UserId INT
AS
	SELECT 
    DetailData.ClinicalLevel,
    Min(DetailData.StartDate) as MinStartDate,
    Max(DetailData.EndDate) as MaxStartDate,
    DetailData.ProgramName,
    Sum(ClinicalWeeks) as ClinicalWeeks,
    Sum(NonClinicalWeeks) as NonClinicalWeeks,
    Sum(EssentialsWeeks) as EssentialsWeeks
FROM
    (
        SELECT
            cl.Name ClinicalLevel,
            gme.StartDate,
            gme.EndDate,
            gme.ProgramName,
            DATEDIFF(WEEK, gme.StartDate, gme.EndDate) + 1 AS TotalWeeks,
            ca.Name ClinicalActivity,
            ca.IsCredit, --0 = NonClinical
            ca.IsEssential,
            case when ca.IsCredit = 1 then DATEDIFF(WEEK, gme.StartDate, gme.EndDate) + 1 else 0 end as ClinicalWeeks,
            case when ca.IsCredit = 0 then DATEDIFF(WEEK, gme.StartDate, gme.EndDate) + 1 else 0 end as NonClinicalWeeks,
            case when ca.IsEssential = 1 then DATEDIFF(WEEK, gme.StartDate, gme.EndDate) + 1 else 0 end as EssentialsWeeks
        FROM
            gme_rotations gme
            inner join clinical_level cl on cl.Id = gme.ClinicalLevelId
            inner join clinical_activity ca on ca.Id = gme.ClinicalActivityId
        WHERE
            UserId = @UserId
    ) as DetailData
GROUP BY 
    DetailData.ClinicalLevel,
    DetailData.ProgramName

RETURN 0
