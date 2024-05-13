select 
    up.UserId,
    up.RetirementStatusId,
CASE
    WHEN LOWER(TRIM(st_val)) LIKE '%emeritus%' or LOWER(TRIM(st_val)) LIKE '%good%standing%' THEN rsrgs.RetirementStatusId
    WHEN ISNULL(ss.UserId, '')!='' THEN  rsr.RetirementStatusId
    ELSE rsnr.RetirementStatusId
END RetiredStatusId,
    ss.UserId,
    ss.st_val,
    ss.status_code
from user_profiles up
LEFT JOIN surgeon_st ss ON ss.UserId=up.UserId AND ss.status_code='NP'
LEFT JOIN retirement_statuses rsnr ON rsnr.Name='Not Retired'
LEFT JOIN retirement_statuses rsr ON rsr.name='Retired'
LEFT JOIN retirement_statuses rsrgs ON rsrgs.name='Retired - In Good Standing'