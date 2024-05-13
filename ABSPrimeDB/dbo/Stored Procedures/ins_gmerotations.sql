CREATE PROCEDURE [dbo].[ins_gmerotations]
	@UserId INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@ClinicalLevelId INT,
	@ClinicalActivityId INT,
	@ProgramName VARCHAR(250),
	@NonSurgicalActivity VARCHAR(500),
	@AlternateInstitutionName VARCHAR(500),
	@IsInternationalRotation BIT,
	@Other VARCHAR(500),
	@FourMonthRotationExplain VARCHAR(8000),
	@NonPrimaryExplain VARCHAR(8000),
	@NonClinicalExplain VARCHAR(8000),
	@CreatedByUserId INT
AS
	INSERT INTO gme_rotations
	(
		UserId,
		StartDate,
		EndDate,
		ClinicalLevelId,
		ClinicalActivityId,
		ProgramName,
		NonSurgicalActivity,
		AlternateInstitutionName,
		IsInternationalRotation,
		Other,
		FourMonthRotationExplain,
		NonPrimaryExplain,
		NonClinicalExplain,
		CreatedByUserId,
		CreatedAtUtc,
		LastUpdatedAtUtc,
		LastUpdatedByUserId
	)
	VALUES
	(
		@UserId,
		@StartDate,
		@EndDate,
		@ClinicalLevelId,
		@ClinicalActivityId,
		@ProgramName,
		@NonSurgicalActivity,
		@AlternateInstitutionName,
		@IsInternationalRotation,
		@Other,
		@FourMonthRotationExplain,
		@NonPrimaryExplain,
		@NonClinicalExplain,
		@CreatedByUserId,
		GetUtcDate(),
		GetUtcDate(),
		@CreatedByUserId
	)

	Declare @Id INT
	Select @Id = SCOPE_IDENTITY()
    EXEC dbo.ins_gme_to_education @UserId, @StartDate, @EndDate, @ClinicalLevelId, @ClinicalActivityId, @ProgramName,  '2024GQ', @AlternateInstitutionName, @IsInternationalRotation, @Other, @Id
RETURN 0
