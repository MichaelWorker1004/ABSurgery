CREATE FUNCTION udf_get_examiner_contact ()
RETURNS TABLE
AS

RETURN (


SELECT     examiner, examiner_name, state, birthdate, email, phone, CASE WHEN ISNULL(mobile_w, '') = '' THEN ISNULL(mobile_h, '') ELSE ISNULL(mobile_w, '') 
                      END AS mobile, addrblock
FROM         (SELECT     candidate AS examiner, dbo.udf_get_namelastfirst(candidate) AS examiner_name, dbo.udf_get_addressblock(candidate) AS addrblock, 
                                              dbo.udf_get_email(candidate) AS email, dbo.udf_get_phone(candidate) AS phone, dbo.udf_get_phone_bytype(candidate, 'O', 'M') AS mobile_w, 
                                              dbo.udf_get_phone_bytype(candidate, 'H', 'M') AS mobile_h, dbo.udf_get_address(candidate, 'state') AS state, birthdate
                       FROM          dbo.surgeon) AS info

)