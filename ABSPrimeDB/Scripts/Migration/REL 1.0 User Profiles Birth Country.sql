UPDATE
    user_profiles
set
    BirthCountry =  country.code
FROM
    user_profiles
INNER JOIN surgeon  ON
    user_profiles.ABSId = surgeon.candidate
INNER JOIN dbo.country ON
    dbo.country.code = surgeon.birthplace
