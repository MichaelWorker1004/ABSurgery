CREATE VIEW V_CA_AUDIT 
AS
SELECT
    TrackNote                   = ISNULL(track.note, ''),
    UserId                      = track.UserId,
    Candidate                   = track.candidate,
    Name                        = dbo.udf_get_namelast(track.candidate),
    Email                       = dbo.udf_get_email(track.candidate),
    Phone                       = dbo.udf_get_phone(track.candidate),
    Addressblock                = dbo.udf_get_addressblock(track.candidate),
    TrackCode                   = track.examcode,
    TrackModified               = ISNULL(CONVERT(CHAR(10), track.modified, 126), ''),
    TrackAppRecd                = ISNULL(CONVERT(CHAR(10), track.app_receive, 126), ''),
    TrackSentForInternalReview  = ISNULL(CONVERT(CHAR(10), track.pre_receive, 126), ''),
    TrackAppApproved            = ISNULL(CONVERT(CHAR(10), track.app_approve, 126), ''),
    TrackAttestRecd             = CASE WHEN LEN(RTRIM(track.sig_receive)) > 0 THEN '1' ELSE '' END,
    TrackAppAutoApproved        = CASE WHEN track.pre_approve = track.app_approve THEN '1' ELSE '' END,
    TrackAutoApproveLockout     = ISNULL(aal.UserId, ''),
    LicNumUnverified            = ISNULL(licnonverify.num_notverified, ''),
    LicNumVerified              = ISNULL(licverify.num_verified, ''),
    ConCertNumCyclesNC          = ISNULL(CAST(moc_eligibility.num_cycles_non_compliant as VARCHAR), 'NA'),
    ConCertReportingDue         = CASE WHEN ISNULL(CAST(moc_eligibility.next_reporting_due AS VARCHAR(8)), '') IS NULL AND ISNULL(moc_eligibility.next_reporting_due, '') ! = 0 THEN '' ELSE CONVERT(CHAR(10), moc_eligibility.next_reporting_due, 126) END,
    FeeTrackTotal               = dbo.udf_get_inv_tot(track.candidate + track.examcode+'A'),
    FeeTrackBalance             = dbo.udf_get_balance(track.candidate + track.examcode+'A'),
    FeeExamTotal                = dbo.udf_get_inv_tot(exam.candidate + exam.examcode+'E'),
    FeeExamBalance              = dbo.udf_get_balance(exam.candidate + exam.examcode+'E'),
    FeeExamRefunded             = CASE WHEN ISNULL(dbo.udf_get_balance(exam.candidate + exam.examcode+'E'),'') ! =  0 AND exam.status= 'N' THEN '1' ELSE '' END,
    CmeAcctNum                  = cme_req.acct_num,
    CmeIsReqMet                 = cme_req.cme_req_met,
    CmeDateFrom                 = cme_req.cme_from,
    CmeDateTo                   = cme_req.cme_to,
    CmeReqCat1                  = cme_req.req_cme_1,
    CmeReqSa                    = cme_req.req_cme_1_sa,
    CmeTotCat1                  = cme_req.tot_cat1,
    CmeTotSa                    = cme_req.tot_cat1_sa,
    OpExpMaxReceiveG            = (select max_oper_receive from dbo.udf_get_oper(track.candidate,'G')),
    OpExpMaxReceiveV            = (select max_oper_receive from dbo.udf_get_oper(track.candidate,'V')),
    OpExpMaxReceiveP            = (select max_oper_receive from dbo.udf_get_oper(track.candidate,'P')),
    OpExpMaxReceiveC            = (select max_oper_receive from dbo.udf_get_oper(track.candidate,'C')),
    Admissible                  = dbo.udf_get_exam_eligibility(track.candidate, track.exam + dbo.udf_type_te(track.exam, track.type)),
    TrackId                     = track.id,
    ExamId                      = exam.id,
    ExamCode                    = exam.examcode,
    ExamStatus                  = ISNULL(exam.status, ''),
    ExamAuthSent                = ISNULL(exam.pearson_sent, ''),
    ExamCcaModule               = ISNULL(exam.module, ''),
    ExamCcaProgress             = IIF(exam.examcode = '2018GR0', CASE WHEN ISNULL(exam.status, '') = 'C' THEN 'COMPLETE' WHEN ISNULL(exam.status, '') = 'R' THEN 'IN PROGRESS' ELSE '' END, ''),
    ExamCcaProgressDescription  = ISNULL(exam.result_comment, ''), 
    ExamCcaPreliminaryResult    = IIF(ISNULL(exam.status, '') = 'C' AND exam.examcode = '2018GR0', isnull(exam.result, ''), ''),	
    ExamCcaRegistrationID       = IIF(exam.examcode = '2018GR0', 'ABS'+exam.candidate+LEFT(exam.examcode,6)+cast(isnull(exam.module,0) as varchar)+cast(isnull(exam.attempt,0) as varchar), ''),	
    PearsonStatus               = ISNULL(exam.pearson_ExamState, ''),
    CertNumActive               = ISNULL(cc_certified.active_count, ''),
    CertAcknow10Yr              = CASE WHEN LEN(RTRIM(track.certnotice_receive)) > 0 THEN '1' ELSE '' END,
    CertExpS                    = expirations.[S],
    CertExpVascS                = expirations.[VascS],
    CertExpSCC                  = expirations.[SCC],
    CertExpPdS                  = expirations.[PdS],
    CertExpHS                   = expirations.[HS],
    CertExpHospPalM             = expirations.[HospPalM],
    CertExpCGSO                 = expirations.[CGSO],
    RefLtr1Sent                 = CASE WHEN LEN(RTRIM(ref0.let_sent)) > 0 THEN '1' ELSE '' END,
    RefLtr1Recd                 = CASE WHEN LEN(RTRIM(ref0.let_rcvd)) > 0 THEN '1' ELSE '' END,
    RefLtr1Approved             = CASE WHEN LEN(RTRIM(ref0.let_approve)) > 0 THEN '1' ELSE '' END,
    RefLtr1NumNos               = IIF(ISNULL(ref0.sameinstitution, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.fullprivileges, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.professionalinteraction, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.personalrelationship, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.acceptableskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.dependable, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.communicationskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.behavior, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.participatesactivities, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.substanceabuse, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.disciplinaryactions, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.recommend, '') = 'Yes', 0, 1) + IIF(ISNULL(ref0.scope, '') = 'Yes', 0, 1),
    RefLtr2Sent                 = CASE WHEN LEN(RTRIM(ref1.let_sent)) > 0 THEN '1' ELSE '' END,
    RefLtr2Recd                 = CASE WHEN LEN(RTRIM(ref1.let_rcvd)) > 0 THEN '1' ELSE '' END,
    RefLtr2Approved             = CASE WHEN LEN(RTRIM(ref1.let_approve)) > 0 THEN '1' ELSE '' END,
    RefLtr2NumNos               = IIF(ISNULL(ref1.sameinstitution, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.fullprivileges, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.professionalinteraction, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.personalrelationship, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.acceptableskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.dependable, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.communicationskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.behavior, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.participatesactivities, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.substanceabuse, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.disciplinaryactions, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.recommend, '') = 'Yes', 0, 1) + IIF(ISNULL(ref1.scope, '') = 'Yes', 0, 1),
    RefLtr3Sent                 = CASE WHEN LEN(RTRIM(ref2.let_sent)) > 0 THEN '1' ELSE '' END,
    RefLtr3Recd                 = CASE WHEN LEN(RTRIM(ref2.let_rcvd)) > 0 THEN '1' ELSE '' END,
    RefLtr3Approved             = CASE WHEN LEN(RTRIM(ref2.let_approve)) > 0 THEN '1' ELSE '' END,
    RefLtr3NumNos               = IIF(ISNULL(ref2.sameinstitution, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.fullprivileges, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.professionalinteraction, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.personalrelationship, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.acceptableskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.dependable, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.communicationskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.behavior, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.participatesactivities, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.substanceabuse, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.disciplinaryactions, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.recommend, '') = 'Yes', 0, 1) + IIF(ISNULL(ref2.scope, '') = 'Yes', 0, 1),
    RefLtr4Sent                 = CASE WHEN LEN(RTRIM(ref3.let_sent)) > 0 THEN '1' ELSE '' END,
    RefLtr4Recd                 = CASE WHEN LEN(RTRIM(ref3.let_rcvd)) > 0 THEN '1' ELSE '' END,
    RefLtr4Approved             = CASE WHEN LEN(RTRIM(ref3.let_approve)) > 0 THEN '1' ELSE '' END,
    RefLtr4NumNos               = IIF(ISNULL(ref3.sameinstitution, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.fullprivileges, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.professionalinteraction, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.personalrelationship, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.acceptableskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.dependable, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.communicationskills, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.behavior, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.participatesactivities, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.substanceabuse, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.disciplinaryactions, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.recommend, '') = 'Yes', 0, 1) + IIF(ISNULL(ref3.scope, '') = 'Yes', 0, 1),
    RefLtrReqMet			  = IIF(rtrim(cc_req.cc_refletnotapproved)='' AND cc_req.cc_refletexpired=0 AND cc_req.cc_refletreceivedinstances>=cc_req.cc_refletinstancesreq,'Y','N')
    
    
    FROM
		track
    LEFT JOIN AppRefLet ref0 ON ref0.UserId = track.UserId AND ref0.let_type = 0 AND ISNULL(ref0.exam_type, '') = ''
    LEFT JOIN AppRefLet ref1 ON ref1.UserId = track.UserId AND ref1.let_type = 1 AND ISNULL(ref1.exam_type, '') = ''
    LEFT JOIN AppRefLet ref2 ON ref2.UserId = track.UserId AND ref2.let_type = 2 AND ISNULL(ref2.exam_type, '') = ''
    LEFT JOIN AppRefLet ref3 ON ref3.UserId = track.UserId AND ref3.let_type = 3 AND ISNULL(ref3.exam_type, '') = ''
    LEFT JOIN moc_eligibility ON moc_eligibility.UserId = track.UserId
    LEFT JOIN exam ON exam.UserId = track.UserId AND track.examcode = exam.trackcode
    CROSS APPLY dbo.udf_get_cme(track.candidate, 'MC') cme_req

    CROSS APPLY dbo.udf_get_cc_req(track.candidate) cc_req
    
    LEFT JOIN (SELECT COUNT(*) num_notverified, UserId, candidate FROM licenses WHERE LicenseExpireDate > GETDATE() AND ( LicenseTypeCode = 'FULL' AND VerifyingOrganization = 'SELF') OR ( ISNULL(LicenseTypeCode, '') = '') GROUP BY UserId, candidate) licnonverify ON track.UserId = licnonverify.UserId
    LEFT JOIN (SELECT COUNT(*) num_verified, UserId , candidate FROM licenses WHERE LicenseExpireDate > GETDATE() AND VerifyingOrganization != 'SELF' AND LicenseTypeCode = 'FULL' GROUP BY UserId, candidate) licverify ON track.UserId = licverify.UserId
    LEFT JOIN (select UserId, candidate, sum(iif(isnull(iscertified, 'N')='Y', 1, 0)) active_count  from v_abms_cert group by UserId, candidate) cc_certified ON track.UserId = cc_certified.UserId
    LEFT JOIN surgeon_st aal ON aal.UserId = track.UserId AND aal.status_code = 'AAL'
    LEFT JOIN surgeon_st dnp ON dnp.UserId = track.UserId AND dnp.status_code IN ('DE')
    LEFT JOIN ( SELECT vac.UserId, vac.candidate ExpirationsCandidateId, CertificateName, MAX(CASE ISNULL(CertificationExpireDate, '') WHEN '' THEN CertificationDuration ELSE CertificationExpireDate END) CertificationExpiration FROM v_abms_cert vac GROUP BY vac.UserId, vac.candidate, vac.CertificateName) AS info PIVOT (MAX(CertificationExpiration) FOR CertificateName IN ([S], [VascS], [SCC], [PdS], [HS], [HospPalM], [CGSO])) as expirations ON track.UserId = expirations.UserId
    WHERE
    	track.candidate    NOT LIKE '8%' AND 
    (
	    (
	    track.year =(SELECT attr FROM mcodes WHERE grp    = 'AY' AND code = 'T')
	    AND track.type = 'R'
	    )
    OR
	    (
		    track.year =(SELECT attr FROM mcodes WHERE grp    = 'AY' AND code = 'M')
		    AND track.exam = 'M'
		     AND track.candidate    NOT LIKE '8%'
	    )
    )