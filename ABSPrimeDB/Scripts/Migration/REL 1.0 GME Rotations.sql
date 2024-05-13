INSERT INTO gme_rotations
(userid,StartDate,EndDate,ClinicalLevelId,ClinicalActivityId,ProgramName,NonSurgicalActivity,AlternateInstitutionName,IsInternationalRotation,Other,CreatedByUserId,LastUpdatedByUserId)
SELECT
       user_profiles.userID,
       education.date_from,
       education.date_to,
       clinical_level.id,
       clinical_activity.id,
       education.prg_name,
       clinical_activity.IsCredit,
       education.institution,
       iif(education.rot_intl='No',0,1),
       education.other,
       @userId,
       @userId
FROM
    user_profiles
LEFT JOIN
    education ON user_profiles.UserId = education.UserId
LEFT JOIN
    clinical_activity ON education.subj_area = clinical_activity.Name
LEFT JOIN
    clinical_level ON education.rot_type = clinical_level.Id
WHERE
    education.rot_type IS NOT NULL
    AND education.rot_type<10
    AND clinical_activity.Id IS NOT NULL