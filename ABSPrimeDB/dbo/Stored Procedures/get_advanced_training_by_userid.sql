CREATE PROCEDURE [dbo].[get_advanced_training_by_userid]
	@UserId int
AS
	SELECT 
		a.Id,
		tt.TrainingType,
		dbo.udf_get_institutionname(p.ProgramId) as InstitutionName,
		pa.City,
		pa.STATE as State,
		a.Other,
		a.StartDate,
		a.EndDate
	FROM
		advancedTraining a
		inner join trainingtype tt on a.TrainingTypeId = tt.Id
		left join program p on a.ProgramId = p.ProgramId and p.approval in ('A', 'C')
		left join program_addresses pa on p.ProgramId = pa.ProgramId
	WHERE
		a.UserId = @UserId

RETURN 0
