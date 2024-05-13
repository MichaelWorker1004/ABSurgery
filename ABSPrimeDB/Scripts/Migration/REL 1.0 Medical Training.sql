INSERT INTO medical_training (UserId,GraduateProfileId,  MedicalSchoolName, MedicalSchoolCity, MedicalSchoolStateId,
                              MedicalSchoolCountryId, DegreeId, MedicalSchoolCompletionYear, ResidencyProgramName,
                              ResidencyCompletionYear, ResidencyProgramOther, CreatedByUserId,LastUpdatedByUserId)
SELECT DISTINCT user_profiles.userid,
       surgeon.school,
       schoolName.st_val,
       schoolCity.st_val,
       schoolState.st_val,
       IIF(schoolState.st_val IN (SELECT state FROM states_info WHERE country='500'),'500','013'),
       CASE
           WHEN surgeon.degree='B' THEN 1
           WHEN surgeon.degree='C' THEN 2
           WHEN surgeon.degree='M' THEN 3
           ELSE 4
       END,
       surgeon.year,
       residencyName.st_val,
       residencyYear.st_val,
       residencyOther.st_val,
       @UserId,
       @UserId
FROM
    user_profiles
LEFT JOIN
    surgeon_st AS schoolName ON user_profiles.UserId=schoolName.userId
LEFT JOIN
    surgeon_st AS schoolCity ON user_profiles.UserId=schoolCity.userId
LEFT JOIN
    surgeon_st AS schoolState ON user_profiles.UserId=schoolState.userId
LEFT JOIN
    surgeon_st AS schoolCountry ON user_profiles.UserId=schoolCountry.userId
LEFT JOIN
    surgeon_st AS residencyName ON user_profiles.UserId=residencyName.userId
LEFT JOIN
    surgeon_st AS residencyYear ON user_profiles.UserId=residencyYear.userId
LEFT JOIN
    surgeon_st AS residencyOther ON user_profiles.UserId=residencyOther.userId
LEFT JOIN
    surgeon ON surgeon.UserId=user_profiles.UserId
left join
    medical_training  on user_profiles.UserId = medical_training.UserId
WHERE
    schoolName.status_code='school'
    AND schoolCity.status_code='schoolCity'
    AND schoolState.status_code='schoolState'
    AND schoolState.status_code IS NOT NULL
    AND surgeon.degree IS NOT NULL
    AND surgeon.school IS NOT NULL
    AND surgeon.school !=''
    AND residencyName.status_code='candidateprogram'
    AND residencyYear.status_code='yearcompletion'
    AND residencyOther.status_code='candidateprogramother'
    AND schoolState.st_val IN (SELECT state FROM states_info)
    and medical_training.UserId is NULL