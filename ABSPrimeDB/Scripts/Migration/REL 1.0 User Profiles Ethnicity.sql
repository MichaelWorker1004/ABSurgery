UPDATE
    user_profiles
set
    Ethnicity =   surgeon.ethnicity
FROM
    user_profiles
INNER JOIN surgeon  ON
    user_profiles.ABSId = surgeon.candidate