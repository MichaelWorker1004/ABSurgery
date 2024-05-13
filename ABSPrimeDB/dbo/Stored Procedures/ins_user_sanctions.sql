CREATE PROCEDURE [dbo].[ins_user_sanctions]
    @UserId int,
    @HadDrugAlchoholTreatment bit =0,
    @HadHospitalPrivilegesDenied bit =0,
    @HadLicenseRestricted bit =0,
    @HadHospitalPrivilegesRestricted bit =0,
    @HadFelonyConviction bit =0,
    @HadCensure bit =0,
    @Explanation VARCHAR(MAX) =NULL,
    @CreatedByUserId int
AS
    INSERT INTO user_sanctions
    (
        UserId,
        HadDrugAlchoholTreatment,
        HadHospitalPrivilegesDenied,
        HadLicenseRestricted,
        HadHospitalPrivilegesRestricted,
        HadFelonyConviction,
        HadCensure,
        Explanation,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    VALUES
    (
        @UserId,
        @HadDrugAlchoholTreatment,
        @HadHospitalPrivilegesDenied,
        @HadLicenseRestricted,
        @HadHospitalPrivilegesRestricted,
        @HadFelonyConviction,
        @HadCensure,
        @Explanation,
        @CreatedByUserId,
        @CreatedByUserId
    )
EXEC [get_user_sanctions_byuserid] @UserId


