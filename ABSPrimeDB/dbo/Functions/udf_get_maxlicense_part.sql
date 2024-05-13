CREATE FUNCTION [dbo].[udf_get_maxlicense_part] (@code char(6),@field varchar (25))
RETURNS varchar(100)
AS
BEGIN
DECLARE @result varchar(100);

SELECT top 1 @result=
	case @field
		when 'id' then cast(id as varchar)
		when 'LicensingStateCode' then LicensingStateCode
		when 'LicenseNumber' then LicenseNumber
		when 'LicenseIssueDate' then case year(isnull(LicenseIssueDate,'')) when 1900 then '' else isnull(convert(varchar(10),LicenseIssueDate,101),'') end
		when 'LicenseExpireDate' then case year(isnull(LicenseExpireDate,'')) when 1900 then '' else isnull(convert(varchar(10),LicenseExpireDate,101),'') end
		when 'LicenseTypeCode' then LicenseTypeCode
		when 'VerifyingOrganization' then VerifyingOrganization
		else ''
	end
	FROM licenses 
WHERE LicenseTypeCode='FULL' 
	AND candidate=@code 
	AND LicenseExpireDate=
		(SELECT MAX(LicenseExpireDate) FROM licenses WHERE candidate=@code and licenseTypeCode='FULL' and VerifyingOrganization!='SELF' AND LicenseExpireDate>getdate())
	AND LicenseExpireDate>getdate()
	AND VerifyingOrganization!='SELF'

RETURN ISNULL(@result,'')		

END