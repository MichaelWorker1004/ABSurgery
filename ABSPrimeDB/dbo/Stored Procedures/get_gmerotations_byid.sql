CREATE PROCEDURE [dbo].[get_gmerotations_byid]
	@Id INT
AS
	SELECT 
		gm.Id,
		UserId,
		StartDate,
		EndDate,
		ClinicalLevelId,
		cl.Name as ClinicalLevel,
		ca.Id as ClinicalActivityId,
		ca.Name as ClinicalActivity,
		ProgramName,
		NonSurgicalActivity,
		AlternateInstitutionName,
		IsInternationalRotation,
		ca.IsEssential,
		ca.IsCredit,
		Other,
		gm.FourMonthRotationExplain,
		gm.NonPrimaryExplain,
		gm.NonClinicalExplain,
		gm.CreatedByUserId,
		gm.CreatedAtUtc,
		gm.LastUpdatedAtUtc,
		gm.LastUpdatedByUserId
	FROM
		gme_rotations gm
		inner join clinical_level cl on cl.id = gm.ClinicalLevelId
		inner join clinical_activity ca on ca.id = gm.ClinicalActivityId
	WHERE 
		gm.id = @Id
RETURN 0
