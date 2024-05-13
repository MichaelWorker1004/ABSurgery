CREATE FUNCTION [dbo].[udf_get_medical_licenses_status](@UserId INT)
RETURNS INT
AS
BEGIN
    --  Not needed for QE, but full unexpired license required to receive certification
    DECLARE @status INT
    SELECT @status= ISNULL(
       (SELECT TOP 1 1 Status FROM licenses l
        LEFT JOIN license_types lt ON lt.Id=l.LicenseTypeId
        WHERE 
            l.UserId=@UserId
            AND l.LicenseExpireDate>GETUTCDATE()
            AND lt.Code='FULL'
            AND UPPER(l.VerifyingOrganization)!='SELF')
    , 0)
    RETURN @status
END