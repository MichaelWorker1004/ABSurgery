UPDATE
    user_profiles
set
    BestLanguageId = LanguageId
FROM
    user_profiles
LEFT JOIN surgeon_st  ON
    user_profiles.ABSId = surgeon_st.candidate AND surgeon_st.status_code='BL'
LEFT JOIN
        dbo.languages ON
            languages.Name=surgeon_st.st_val
WHERE LanguageId IS NOT NULL