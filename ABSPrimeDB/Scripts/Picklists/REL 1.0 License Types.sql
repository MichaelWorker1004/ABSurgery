INSERT INTO license_types (code, name, createdbyuserid, lastupdatedbyuserid)
SELECT
    mcodes.code,
    mcodes.Descr,
    @UserId,
    @UserId
FROM
    mcodes
WHERE
    mcodes.Grp='LT'