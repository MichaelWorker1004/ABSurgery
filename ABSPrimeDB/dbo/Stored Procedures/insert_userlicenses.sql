CREATE PROCEDURE [dbo].[insert_userlicenses]
	@UserId INT,
    @IssuingStateId VARCHAR(2),
    @LicenseNumber VARCHAR(20),
    @LicenseTypeId INT,
    @ReportingOrganization VARCHAR(50),
    @IssueDate DATETIME,
    @ExpireDate DATETIME
AS
	INSERT INTO
		dbo.licenses
		(
			UserId,
			LicensingStateCode,
            LicenseNumber,
            LicenseTypeId,
            VerifyingOrganization,
            LicenseIssueDate,
            LicenseExpireDate
		)
		VALUES
		(
            @UserId,
            @IssuingStateId,
            @LicenseNumber,
            @LicenseTypeId,
            @ReportingOrganization,
            @IssueDate,
            @ExpireDate
		)

    DECLARE @Id INT
	SET @Id = SCOPE_IDENTITY()

	EXEC dbo.get_userlicenses_byid @Id