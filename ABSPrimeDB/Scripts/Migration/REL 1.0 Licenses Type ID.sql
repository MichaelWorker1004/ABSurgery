UPDATE
    licenses
SET
    licenseTypeId=license_types.id
FROM
    licenses
INNER JOIN
    license_types ON license_types.code=licenses.LicenseTypeCode