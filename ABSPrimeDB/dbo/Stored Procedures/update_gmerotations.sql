CREATE PROCEDURE [dbo].[update_gmerotations]
	@Id INT,
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
	@LastUpdatedByUserId INT
AS
	UPDATE 
		gme_rotations
	SET
		UserId = @UserId,
		StartDate = @StartDate,
		EndDate = @EndDate,
		ClinicalLevelId = @ClinicalLevelId,
		ClinicalActivityId = @ClinicalActivityId,
		ProgramName = @ProgramName,
		NonSurgicalActivity = @NonSurgicalActivity,
		AlternateInstitutionName = @AlternateInstitutionName,
		IsInternationalRotation = @IsInternationalRotation,
		Other = @Other,
		FourMonthRotationExplain = @FourMonthRotationExplain,
		NonPrimaryExplain = @NonPrimaryExplain,
		NonClinicalExplain = @NonClinicalExplain,
		LastUpdatedAtUtc = GetUtcDate(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE 
		Id = @Id
    EXEC dbo.update_gme_to_education @UserId, @StartDate, @EndDate, @ClinicalLevelId, @ClinicalActivityId, @ProgramName,  '2024GQ', @AlternateInstitutionName, @IsInternationalRotation, @Other, @Id
RETURN 0
