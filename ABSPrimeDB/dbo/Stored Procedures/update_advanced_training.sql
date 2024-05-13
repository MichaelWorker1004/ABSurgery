CREATE PROCEDURE [dbo].[update_advanced_training]
	@Id int,
	@UserId int,
	@TrainingTypeId int,
	@ProgramId int,
	@Other varchar(100),
	@StartDate date,
	@EndDate date,
	@LastUpdatedByUserId int
AS
	SET NOCOUNT ON

	Update [dbo].[advancedTraining] SET
		UserId = @UserId,
		TrainingTypeId = @TrainingTypeId,
		ProgramId = @ProgramId,
		Other = @Other,
		StartDate = @StartDate,
		EndDate = @EndDate,
		LastUpdatedAtUtc = GETUTCDATE(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @Id

	EXEC [dbo].[get_advanced_training_by_trainingid] @Id

RETURN 0
