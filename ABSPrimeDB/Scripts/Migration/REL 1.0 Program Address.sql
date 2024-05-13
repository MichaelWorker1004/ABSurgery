INSERT INTO  program_addresses (ProgramID,street1,Street2,City,STATE,ZipCode,Country,AddressType,IsMailingAddress,CreatedByUserId,LastUpdatedByUserId)
SELECT
    program.programid,
    address.street1,
    address.street2,
    address.city,
    address.state,
    address.zip,
    address.street4,
    UPPER(address.type),
    iif(address.mail='M',1,0),
    @UserId,
    @UserId
FROM
program
LEFT JOIN
    address ON program.number+program.exam=address.code
WHERE
    status = 'P'
    AND type!='A'