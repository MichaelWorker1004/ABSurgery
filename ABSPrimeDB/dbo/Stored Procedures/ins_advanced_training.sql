CREATE PROCEDURE [dbo].[ins_advanced_training]
	@UserId int,
	@TrainingTypeId int,
	@ProgramId int,
	@Other varchar(100),
	@StartDate date,
	@EndDate date,
	@CreatedByUserId int
AS
	SET NOCOUNT ON
	DECLARE @Id INT

	INSERT INTO advancedTraining
	(UserId, TrainingTypeId, ProgramId, Other, StartDate, EndDate, CreatedByUserId, LastUpdatedByUserId)
	VALUES
	(@UserId, @TrainingTypeId, @ProgramId, @Other, @StartDate, @EndDate, @CreatedByUserId, @CreatedByUserId);

	SET @Id = SCOPE_IDENTITY();

	EXEC [dbo].[get_advanced_training_by_trainingid] @Id

RETURN 0
