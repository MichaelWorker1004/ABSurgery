UPDATE
    user_profiles
set
    FirstLanguageId = LanguageId
FROM
    user_profiles
LEFT JOIN surgeon_st  ON
    user_profiles.ABSId = surgeon_st.candidate AND surgeon_st.status_code='FL'
LEFT JOIN
        dbo.languages ON
            languages.Name=surgeon_st.st_val
WHERE LanguageId IS NOT NULL