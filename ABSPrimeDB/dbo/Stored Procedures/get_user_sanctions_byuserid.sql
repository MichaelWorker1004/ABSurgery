CREATE PROCEDURE [dbo].[get_user_sanctions_byuserid]
	@UserId int
AS
SELECT
	Id,
	UserId,
	HadDrugAlchoholTreatment,
	HadHospitalPrivilegesDenied,
	HadLicenseRestricted,
	HadHospitalPrivilegesRestricted,
	HadFelonyConviction,
	HadCensure,
	Explanation,
	CreatedByUserId,
	CreatedAtUtc,
	LastUpdatedAtUtc,
	LastUpdatedByUserId
	FROM
		user_sanctions
	WHERE
		UserId = @UserId

RETURN 0
