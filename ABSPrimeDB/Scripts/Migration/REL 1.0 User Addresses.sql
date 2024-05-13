INSERT INTO  addresses (UserId,street1,Street2,City,STATE,ZipCode,Country,AddressType,IsMailingAddress,CreatedByUserId,LastUpdatedByUserId)
SELECT
    user_profiles.userId,
    address.street1,
    address.street2,
    address.city,
    address.state,
    address.zip,
    country.code,
    UPPER(address.type),
    iif(address.mail='M',1,0),
    @UserId,
    @UserId
FROM
user_profiles
LEFT JOIN
    address ON user_profiles.ABSId=address.code
LEFT JOIN
    states_info ON address.state=states_info.state
LEFT JOIN
    country ON states_info.country=country.code

WHERE
    status = 'S'