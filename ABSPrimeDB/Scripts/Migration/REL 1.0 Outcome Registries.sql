INSERT INTO outcome_registries(UserId, SurgeonSpecificRegistry, RegisteredWithACHQC, RegisteredWithCESQIP, RegisteredWithMBSAQIP, RegisteredWithABA, RegisteredWithASBS, RegisteredWithMSQC, RegisteredWithABMS, RegisteredWithNCDB, RegisteredWithRQRS, RegisteredWithNSQIP, RegisteredWithNTDB, RegisteredWithSTS, RegisteredWithTQIP, RegisteredWithUNOS, RegisteredWithNCDR,RegisteredWithSVS, RegisteredWithELSO,RegisteredWithSSR, CreatedByUserId, LastUpdatedByUserId)
SELECT
    user_profiles.userid,
    SurgeonSpecificRegistry.st_val,
    IIF(RegisteredWithACHQC.st_val='Yes',1,0),
    IIF(RegisteredWithCESQIP.st_val='Yes',1,0),
    IIF(RegisteredWithMBSAQIP.st_val='Yes',1,0),
    IIF(RegisteredWithABA.st_val='Yes',1,0),
    IIF(RegisteredWithASBS.st_val='Yes',1,0),
    IIF(RegisteredWithMSQC.st_val='Yes',1,0),
    IIF(RegisteredWithABMS.st_val='Yes',1,0),
    IIF(RegisteredWithNCDB.st_val='Yes',1,0),
    IIF(RegisteredWithRQRS.st_val='Yes',1,0),
    IIF(RegisteredWithNSQIP.st_val='Yes',1,0),
    IIF(RegisteredWithNTDB.st_val='Yes',1,0),
    IIF(RegisteredWithSTS.st_val='Yes',1,0),
    IIF(RegisteredWithTQIP.st_val='Yes',1,0),
    IIF(RegisteredWithUNOS.st_val='Yes',1,0),
    IIF(RegisteredWithNCDR.st_val='Yes',1,0),
    IIF(RegisteredWithSVS.st_val='Yes',1,0),
    IIF(RegisteredWithELSO.st_val='Yes',1,0),
    IIF(RegisteredWithSSR.st_val='Yes',1,0),
    @UserId,
    @UserId
FROM
    user_profiles
LEFT JOIN surgeon_st SurgeonSpecificRegistry on user_profiles.UserId=SurgeonSpecificRegistry.UserId and SurgeonSpecificRegistry.status_code='outcomes_other'
LEFT JOIN surgeon_st RegisteredWithACHQC on user_profiles.UserId=RegisteredWithACHQC.UserId and RegisteredWithACHQC.status_code='outcomes_achqc'
LEFT JOIN surgeon_st RegisteredWithCESQIP on user_profiles.UserId=RegisteredWithCESQIP.UserId and RegisteredWithCESQIP.status_code='outcomes_cesqip'
LEFT JOIN surgeon_st RegisteredWithMBSAQIP on user_profiles.UserId=RegisteredWithMBSAQIP.UserId and RegisteredWithMBSAQIP.status_code='outcomes_mbsaqip'
LEFT JOIN surgeon_st RegisteredWithABA on user_profiles.UserId=RegisteredWithABA.UserId and RegisteredWithABA.status_code='outcomes_aba'
LEFT JOIN surgeon_st RegisteredWithASBS on user_profiles.UserId=RegisteredWithASBS.UserId and RegisteredWithASBS.status_code='outcomes_asbs'
LEFT JOIN surgeon_st RegisteredWithMSQC on user_profiles.UserId=RegisteredWithMSQC.UserId and RegisteredWithMSQC.status_code='outcomes_msqc'
LEFT JOIN surgeon_st RegisteredWithABMS on user_profiles.UserId=RegisteredWithABMS.UserId and RegisteredWithABMS.status_code='outcomes_abms_portfolio'
LEFT JOIN surgeon_st RegisteredWithNCDB on user_profiles.UserId=RegisteredWithNCDB.UserId and RegisteredWithNCDB.status_code='outcomes_ncdb'
LEFT JOIN surgeon_st RegisteredWithRQRS on user_profiles.UserId=RegisteredWithRQRS.UserId and RegisteredWithRQRS.status_code='outcomes_rqrs'
LEFT JOIN surgeon_st RegisteredWithNSQIP on user_profiles.UserId=RegisteredWithNSQIP.UserId and RegisteredWithNSQIP.status_code='outcomes_nsqip'
LEFT JOIN surgeon_st RegisteredWithNTDB on user_profiles.UserId=RegisteredWithNTDB.UserId and RegisteredWithNTDB.status_code='outcomes_ntdb'
LEFT JOIN surgeon_st RegisteredWithSTS on user_profiles.UserId=RegisteredWithSTS.UserId and RegisteredWithSTS.status_code='outcomes_sts'
LEFT JOIN surgeon_st RegisteredWithTQIP on user_profiles.UserId=RegisteredWithTQIP.UserId and RegisteredWithTQIP.status_code='outcomes_tqip'
LEFT JOIN surgeon_st RegisteredWithUNOS on user_profiles.UserId=RegisteredWithUNOS.UserId and RegisteredWithUNOS.status_code='outcomes_unos'
LEFT JOIN surgeon_st RegisteredWithNCDR on user_profiles.UserId=RegisteredWithNCDR.UserId and RegisteredWithNCDR.status_code='outcomes_ncdr'
LEFT JOIN surgeon_st RegisteredWithSVS on user_profiles.UserId=RegisteredWithSVS.UserId and RegisteredWithSVS.status_code='outcomes_svs'
LEFT JOIN surgeon_st RegisteredWithELSO on user_profiles.UserId=RegisteredWithELSO.UserId and RegisteredWithELSO.status_code='outcomes_elso'
LEFT JOIN surgeon_st RegisteredWithSSR on user_profiles.UserId=RegisteredWithSSR.UserId and RegisteredWithSSR.status_code='outcomes_ssr'
WHERE RegisteredWithABA.st_val IS NOT NULL