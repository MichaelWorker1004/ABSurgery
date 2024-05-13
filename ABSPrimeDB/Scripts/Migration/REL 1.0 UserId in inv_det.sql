UPDATE dbo.inv_det
SET inv_det.UserId = user_profiles.UserId
FROM user_profiles
WHERE user_profiles.ABSId = substring(inv_det.inv_num,1,6)
AND inv_det.UserId IS NULL