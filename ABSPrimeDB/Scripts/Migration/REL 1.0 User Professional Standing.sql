INSERT INTO user_professional_standing (UserId, PrimaryPracticeId, OrganizationTypeId, ExplanationOfNonPrivileges, ExplanationOfNonClinicalActivities,CreatedByUserId, LastUpdatedByUserId)
SELECT
    user_profiles.userId,
    Primary_Practice.id,
    Organization_Type.id,
    surgeon.practicedesc,
    Nonclinical.st_val,
    @UserId,
    @UserId
FROM
    user_profiles
LEFT JOIN surgeon_st PrimaryPractice ON PrimaryPractice.UserId=user_profiles.userId AND PrimaryPractice.status_code='primarypracticetraining'
LEFT JOIN surgeon_st Organization ON Organization.UserId=user_profiles.userId AND Organization.status_code='primaryorganizationtype'
LEFT JOIN surgeon_st Nonclinical ON Nonclinical.UserId=user_profiles.userId AND Nonclinical.status_code='nonclinicalactivities'
LEFT JOIN Organization_Type ON Organization_Type.Type=Organization.st_val
LEFT JOIN Primary_Practice ON Primary_Practice.Practice=PrimaryPractice.st_val
LEFT JOIN surgeon ON surgeon.UserId=user_profiles.userId
