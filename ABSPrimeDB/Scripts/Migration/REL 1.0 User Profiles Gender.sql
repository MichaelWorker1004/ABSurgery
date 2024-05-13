UPDATE
    user_profiles
set
    GenderId =    genders.GenderId
FROM
    user_profiles
INNER JOIN surgeon  ON
    user_profiles.ABSId = surgeon.candidate
LEFT JOIN genders ON
    IIF(surgeon.sex='M', 'Male',IIF(surgeon.sex='F','Female',''))=genders.Name
