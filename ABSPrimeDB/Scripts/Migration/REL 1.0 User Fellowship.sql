INSERT INTO user_fellowships (UserId, ProgramName, CompletionYear, ProgramOther, CreatedByUserId, LastUpdatedByUserId)
SELECT
       user_profiles.UserId,
       programName.st_val,
       programYear.st_val,
       programOther.st_val,
       @UserId,
       @UserId
FROM
    user_profiles
LEFT JOIN
    surgeon_st AS programName ON user_profiles.UserId=programName.userId
LEFT JOIN
    surgeon_st AS programYear ON user_profiles.UserId=programYear.userId
LEFT JOIN
    surgeon_st AS programOther ON user_profiles.UserId=programOther.userId
WHERE
    programName.status_code IN ('candidateprogram_cc','candidateprogram_oq','candidateprogram_pq','candidateprogram_vq')
    AND programYear.status_code IN ('yearcompletion_cc','yearcompletion_oq','yearcompletion_pq','yearcompletion_vq')
    AND programOther.status_code IN ('candidateprogramother_cc','candidateprogramother_oq','candidateprogramother_pq','candidateprogramother_vq')
    AND programYear.st_val IS NOT NULL
    AND programYear.st_val != ''
    AND programName.st_val IS NOT NULL
    AND programName.st_val != ''