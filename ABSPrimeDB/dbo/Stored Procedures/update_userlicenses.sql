CREATE PROCEDURE [dbo].[update_userlicenses]
	@LicenseId INT,
	@UserId INT,
    @IssuingStateId VARCHAR(2),
    @LicenseNumber VARCHAR(20),
    @LicenseTypeId INT,
    @ReportingOrganization VARCHAR(50),
    @IssueDate DATETIME,
    @ExpireDate DATETIME
AS
	UPDATE
		dbo.licenses
	SET
		UserId                  = @UserId,
        LicensingStateCode      = @IssuingStateId,
        LicenseNumber           = @LicenseNumber,
        LicenseTypeId           = @LicenseTypeId,
        VerifyingOrganization   = @ReportingOrganization,
        LicenseIssueDate        = @IssueDate,
        LicenseExpireDate       = @ExpireDate
	WHERE
		Id = @LicenseId

	EXEC dbo.get_userlicenses_byid @LicenseId