CREATE FUNCTION [dbo].[udf_get_fsmblicense] (@code char(6),@delim varchar(5))
RETURNS varchar(100)
AS
BEGIN
DECLARE @result varchar(100);
SELECT top 1 @result='FSMB Lic#:' + LicenseNumber+'['+ LicensingStateCode+']'+@delim+'Exp:'+case year(isnull(LicenseExpireDate,'')) when 1900 then '' else isnull(convert(varchar(10),LicenseExpireDate,101),'') end
	FROM licenses 
WHERE LicenseTypeCode='FULL' 
	AND candidate=@code 
	AND LicenseExpireDate=
		(SELECT MAX(LicenseExpireDate) FROM licenses WHERE candidate=@code and licenseTypeCode='FULL' AND VerifyingOrganization!='SELF' AND LicenseExpireDate>getdate())
	AND LicenseExpireDate>getdate()
	AND VerifyingOrganization!='SELF'

RETURN ISNULL(@result,'')		

END