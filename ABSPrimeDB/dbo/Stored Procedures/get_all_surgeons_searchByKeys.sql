﻿CREATE PROCEDURE [dbo].[get_all_surgeons_searchByKeys]
 @LastName varchar(30),
 @FirstName varchar(30) = null,
 @SpecialtyCode char(1) = null
AS
BEGIN
SELECT DISTINCT TOP 21 
    user_profiles.NPI Npi,
    user_profiles.FirstName+IIF(user_profiles.MiddleName IS NULL,' ',' '+user_profiles.MiddleName+' ')+user_profiles.LastName as FullName,
    user_profiles.FirstName,
    isnull(user_profiles.MiddleName,'') MiddleName,
    user_profiles.UserId,
    user_profiles.LastName,
    addresses.State,
    user_profiles.BirthDate
FROM
    user_profiles
    LEFT JOIN addresses on addresses.UserId=user_profiles.UserId and addresses.IsMailingAddress=1
    left  join (select distinct UserId, exam from v_abms_cert) c on user_profiles.UserId = c.userid
WHERE user_profiles.UserId NOT IN
(
	SELECT UserId FROM surgeon_st WHERE status_code='DE' and surgeon_st.UserId IS NOT NULL
)
and user_profiles.LastName = @LastName
and user_profiles.FirstName = IsNull(@FirstName, user_profiles.FirstName)
and c.exam = IsNull(@SpecialtyCode, c.exam)
order by FullName ASC
END