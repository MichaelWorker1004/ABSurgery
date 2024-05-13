CREATE PROCEDURE [dbo].[get_gmerotations_byuserid]
	@UserId INT
AS
	SELECT 
		gme_rotations.Id,
		StartDate,
		EndDate,
		ProgramName,
		AlternateInstitutionName,
		cl.Id as ClinicalLevelId,
		cl.Name as ClinicalLevel,
		ca.Name as ClinicalActivity,
		ca.IsEssential,
		ca.IsCredit,
		Other,
		NonSurgicalActivity,
		IsInternationalRotation
	FROM
		gme_rotations
		inner join clinical_level cl on cl.id = gme_rotations.ClinicalLevelId
		inner join clinical_activity ca on ca.id = gme_rotations.ClinicalActivityId
	WHERE
		UserId = @UserId

RETURN 0
