UPDATE dbo.fee_received
SET fee_received.UserId = user_profiles.UserId
FROM user_profiles
WHERE user_profiles.ABSId = substring(fee_received.inv_num,1,6)
AND fee_received.UserId IS NULL