CREATE PROCEDURE [dbo].[get_user_professional_standing_byuserid]
	@UserId int
AS
SELECT
	user_professional_standing.Id,
	user_professional_standing.UserId,
	PrimaryPracticeId,
	Practice as 'PrimaryPractice',
	OrganizationTypeId,
	type as OrganizationType,
	ExplanationOfNonPrivileges,
	ExplanationOfNonClinicalActivities,
	IIF(ISNULL(clinically_active.UserId, '')!='', 1, 0) ClinicallyActive,
	user_professional_standing.CreatedByUserId,
	user_professional_standing.CreatedAtUtc,
	user_professional_standing.LastUpdatedAtUtc,
	user_professional_standing.LastUpdatedByUserId
	FROM
		user_professional_standing
	LEFT JOIN
		    Organization_Type on Organization_Type.Id = user_professional_standing.OrganizationTypeId
	LEFT JOIN
		    Primary_Practice on Primary_Practice.Id = user_professional_standing.PrimaryPracticeId
	LEFT JOIN 
			(SELECT DISTINCT ep.UserId FROM exam_pass ep LEFT JOIN user_clinically_inactive uci ON uci.UserId=ep.UserId and ep.ExamSpecialtyId=uci.ExamSpecialtyId WHERE uci.UserId IS NULL and ep.expiration>=YEAR(getdate())) clinically_active ON clinically_active.UserId=user_professional_standing.UserId
	WHERE
		user_professional_standing.UserId = @UserId
		
RETURN 0
