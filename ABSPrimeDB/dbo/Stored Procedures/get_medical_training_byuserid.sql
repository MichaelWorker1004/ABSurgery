CREATE PROCEDURE [dbo].[get_medical_training_byuserid]
	@UserId INT
AS
	SELECT 
		 mt.Id
		,UserId
		,gp.GraduateProfileId
		,gp.Description as GraduateProfileDescription
		,MedicalSchoolName
		,MedicalSchoolCity
		,MedicalSchoolStateId
		,s.description as MedicalSchoolStateName
		,MedicalSchoolCountryId
		,c.description as MedicalSchoolCountryName
		,DegreeId
		,d.description as DegreeName
		,MedicalSchoolCompletionYear
		,ResidencyProgramName
		,ResidencyCompletionYear
		,ResidencyProgramOther
		,mt.CreatedAtUtc
		,mt.CreatedByUserId
		,mt.LastUpdatedByUserId
		,mt.LastUpdatedAtUtc
	FROM 
		medical_training mt
		inner join graduate_profile gp on mt.GraduateProfileId = gp.GraduateProfileId
		left join states_info s on mt.MedicalSchoolStateId = s.state
		inner join country c on mt.MedicalSchoolCountryId = c.code
		inner join degree d on mt.DegreeId = d.degree_id
	WHERE 
		UserId = @UserId

