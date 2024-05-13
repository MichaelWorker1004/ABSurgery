CREATE PROCEDURE [dbo].[update_medical_training]
	@Id INT,
	@UserId INT,
	@GraduateProfileId INT,
	@MedicalSchoolName VARCHAR(100),
	@MedicalSchoolCity VARCHAR(50),
	@MedicalSchoolStateId CHAR(2),
	@MedicalSchoolCountryId CHAR(3),
	@DegreeId INT,
	@MedicalSchoolCompletionYear CHAR(4),
	@ResidencyProgramName VARCHAR(500),
	@ResidencyCompletionYear CHAR(4),
	@ResidencyProgramOther VARCHAR(500),
	@LastUpdatedByUserId INT
AS
	UPDATE medical_training
	Set 
		UserId = @UserId, 
		GraduateProfileId = @GraduateProfileId, 
		MedicalSchoolName = @MedicalSchoolName, 
		MedicalSchoolCity = @MedicalSchoolCity, 
		MedicalSchoolStateId = @MedicalSchoolStateId, 
		MedicalSchoolCountryId = @MedicalSchoolCountryId, 
		DegreeId = @DegreeId, 
		MedicalSchoolCompletionYear = @MedicalSchoolCompletionYear, 
		ResidencyProgramName = @ResidencyProgramName, 
		ResidencyCompletionYear = @ResidencyCompletionYear, 
		ResidencyProgramOther = @ResidencyProgramOther, 
		LastUpdatedByUserId = @LastUpdatedByUserId, 
		LastUpdatedAtUtc = GetUtcDate()
	WHERE Id = @Id

	--get the data for the user
	EXEC dbo.get_medical_training_byuserid @UserId

RETURN 0
