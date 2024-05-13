INSERT INTO user_sanctions (UserId, HadDrugAlchoholTreatment, HadHospitalPrivilegesDenied, HadLicenseRestricted, HadHospitalPrivilegesRestricted,
                                        HadFelonyConviction, HadCensure, Explanation, CreatedByUserId, LastUpdatedByUserId)
SELECT
    user_profiles.userId,
    IIF(SanctionDrugs.st_val='Yes',1,0),
    IIF(SanctionHospitalPrivileges.st_val='Yes',1,0),
    IIF(SanctionLicense.st_val='Yes',1,0),
    IIF(SanctionDisciplinary.st_val='Yes',1,0),
    IIF(SanctionFelony.st_val='Yes',1,0),
    IIF(SanctionCensured.st_val='Yes',1,0),
    SanctionExplanation.st_val,
    @UserId,
    @UserId
FROM
    user_profiles
LEFT JOIN surgeon_st SanctionDrugs ON SanctionDrugs.UserId=user_profiles.userId AND SanctionDrugs.status_code='sanctionsethicsdrugalcohol'
LEFT JOIN surgeon_st SanctionHospitalPrivileges ON SanctionHospitalPrivileges.UserId=user_profiles.userId AND SanctionHospitalPrivileges.status_code='sanctionsethicshospitalprivileges'
LEFT JOIN surgeon_st SanctionLicense ON SanctionLicense.UserId=user_profiles.userId AND SanctionLicense.status_code='sanctionsethicslicensingsanctions'
LEFT JOIN surgeon_st SanctionDisciplinary ON SanctionDisciplinary.UserId=user_profiles.userId AND SanctionDisciplinary.status_code='sanctionsethicsdisciplinaryaction'
LEFT JOIN surgeon_st SanctionFelony ON SanctionFelony.UserId=user_profiles.userId AND SanctionFelony.status_code='sanctionsethicsfelony'
LEFT JOIN surgeon_st SanctionCensured ON SanctionCensured.UserId=user_profiles.userId AND SanctionCensured.status_code='sanctionsethicscensured'
LEFT JOIN surgeon_st SanctionExplanation ON SanctionExplanation.UserId=user_profiles.userId AND SanctionExplanation.status_code='sanctionsethicsdescribe'
