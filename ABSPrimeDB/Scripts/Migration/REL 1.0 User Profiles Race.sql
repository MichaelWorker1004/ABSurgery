UPDATE
    user_profiles
set
    Race =   surgeon.race
FROM
    user_profiles
INNER JOIN surgeon  ON
    user_profiles.ABSId = surgeon.candidate