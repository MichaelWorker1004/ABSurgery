CREATE PROCEDURE [dbo].[get_advanced_training_by_trainingid]
	@Id int
AS
	SELECT 
		a.Id,
		a.UserId,
		a.TrainingTypeId,
		tt.TrainingType,
		a.ProgramId,
		dbo.udf_get_institutionname(p.ProgramId) as InstitutionName,
		pa.City,
		pa.STATE as State,
		a.Other,
		a.StartDate,
		a.EndDate,
		a.CreatedByUserId,
		a.CreatedAtUtc,
		a.LastUpdatedAtUtc,
		a.LastUpdatedByUserId
	FROM
		advancedTraining a
		inner join trainingtype tt on a.TrainingTypeId = tt.Id
		left join program p on a.ProgramId = p.ProgramId and p.approval in ('A', 'C')
		left join program_addresses pa on p.ProgramId = pa.ProgramId
	WHERE
		a.Id = @Id

RETURN 0
