CREATE PROCEDURE [dbo].[ins_medical_training]
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
	@CreatedByUserId INT
AS
	SET NOCOUNT ON
	
	INSERT INTO medical_training
		(UserId, 
		GraduateProfileId, 
		MedicalSchoolName, 
		MedicalSchoolCity, 
		MedicalSchoolStateId, 
		MedicalSchoolCountryId, 
		DegreeId, 
		MedicalSchoolCompletionYear, 
		ResidencyProgramName, 
		ResidencyCompletionYear, 
		ResidencyProgramOther, 
		CreatedByUserId,
		LastUpdatedByUserId)
	Values
		(@UserId, 
		@GraduateProfileId, 
		@MedicalSchoolName, 
		@MedicalSchoolCity, 
		@MedicalSchoolStateId, 
		@MedicalSchoolCountryId, 
		@DegreeId, 
		@MedicalSchoolCompletionYear, 
		@ResidencyProgramName, 
		@ResidencyCompletionYear, 
		@ResidencyProgramOther, 
		@CreatedByUserId,
		@CreatedByUserId)

	--Get the data for the user
	EXEC dbo.get_medical_training_byuserid @UserId

RETURN 0
