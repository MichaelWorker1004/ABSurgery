UPDATE user_profiles
SET CountryCitizenship =  country.code
FROM user_profiles
INNER JOIN country ON country.description = user_profiles.CountryCitizenship


UPDATE
    user_profiles
set
    CountryCitizenship = country.code
FROM
    user_profiles
INNER JOIN surgeon_st  ON
    user_profiles.ABSId = surgeon_st.candidate AND surgeon_st.status_code='citizenshipcountry'
INNER JOIN dbo.country ON
    dbo.country.description = surgeon_st.st_val
