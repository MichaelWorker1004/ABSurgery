CREATE PROCEDURE [dbo].[get_userlicenses_byid]
	@LicenseId INT
AS
	SELECT 
		l.Id as LicenseId,
		l.UserId,
        l.LicensingStateCode AS IssuingStateId,
		st.state_description AS IssuingState,
		l.LicenseNumber,
        l.LicenseTypeId,
        lt.Name AS LicenseType,
        l.LicenseIssueDate AS IssueDate,
        l.LicenseExpireDate AS ExpireDate,
        l.VerifyingOrganization AS ReportingOrganization
	FROM
		dbo.licenses l
		LEFT JOIN dbo.license_types lt ON lt.Id = l.LicenseTypeId
        LEFT JOIN dbo.states_info st ON st.state = l.LicensingStateCode
	WHERE
		l.Id = @LicenseId