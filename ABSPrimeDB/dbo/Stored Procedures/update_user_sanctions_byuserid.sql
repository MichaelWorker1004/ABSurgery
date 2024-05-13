CREATE PROCEDURE [dbo].[update_user_sanctions_byuserid]
    @UserId int,
    @HadDrugAlchoholTreatment bit =0,
    @HadHospitalPrivilegesDenied bit =0,
    @HadLicenseRestricted bit =0,
    @HadHospitalPrivilegesRestricted bit =0,
    @HadFelonyConviction bit =0,
    @HadCensure bit =0,
    @Explanation VARCHAR(MAX) =NULL,
    @LastUpdatedByUserId int
AS
	Update [dbo].[user_sanctions] SET
		HadDrugAlchoholTreatment = @HadDrugAlchoholTreatment,
		HadHospitalPrivilegesDenied = @HadHospitalPrivilegesDenied,
		HadLicenseRestricted = @HadLicenseRestricted,
		HadHospitalPrivilegesRestricted = @HadHospitalPrivilegesRestricted,
		HadFelonyConviction = @HadFelonyConviction,
		HadCensure = @HadCensure,
		Explanation = @Explanation,
		LastUpdatedAtUtc = GetUtcDate(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		UserId = @UserId

EXEC [dbo].[get_user_sanctions_byuserid] @UserId