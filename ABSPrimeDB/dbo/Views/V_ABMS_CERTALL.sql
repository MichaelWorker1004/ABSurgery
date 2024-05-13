CREATE VIEW [dbo].[V_ABMS_CERTALL] AS
select * from (
	select
		exam_pass.UserId, 
		exam_pass.candidate,
		case exam_pass.type 
			when 'W' then 'Initial' 
			when 'O' then 'Initial' 
			when 'B' then 'FPD'
			else 'Recertification' 
		end CertificationOccurrence,
		case exam_pass.durationtype 
			when 'L' then 'Lifetime' 
			when 'T' then 'Time-limited'
			when 'C' then 'Continuous' 
			else ''
		end CertificationDuration,
		case 
			when exam_pass.exam in ('G','V') then 'General Certificate'
			else 'Subcertificate'
		end CertificateType,
		case 
			when exam_pass.exam='G' then 'S' 
			when exam_pass.exam='C' then 'SCC' 
			when exam_pass.exam='H' then 'HS' 
			when exam_pass.exam='O' then 'CGSO'
			when exam_pass.exam='P' then 'PdS' 
			when exam_pass.exam='V' then 'VascS' 
			when exam_pass.exam='Z' then 'HospPalM' 
		end CertificateName,
		iif(expiration is null,'',isnull(convert(varchar(10),exam_pass.date,20),'')) CertificationIssueDate,
		case 
			when isnull(exam_pass.expiration,0) in (0,2099) or durationtype='L' then '' 
			when exam_pass.expiration<2014 then convert(varchar(10),cast(exam_pass.expiration as varchar(4))+'-07-01',20) 
			else convert(varchar(10),cast(exam_pass.expiration as varchar(4))+'-12-31',20) 
		end CertificationExpireDate,
		case 
			when isnull(exam_pass.expiration,0)=0 then 0 
			when exam_pass.expiration<2014 then DATEDIFF(day,getdate(),cast('07/01/'+cast(exam_pass.expiration as varchar(4)) as datetime))
			else DATEDIFF(day,getdate(),cast('01/01/'+cast(exam_pass.expiration+1 as varchar(4)) as datetime)) 
		end Expired,
		case when dscpl_action.effective=1 and dscpl_action.dscpl_code='CR' then 'Y' else '' end Revoked,
		case when dscpl_action.effective=1 and dscpl_action.dscpl_code='CS' then 'Y' else '' end Suspended,
		case when dscpl_action.effective=1 and dscpl_action.dscpl_code='PR' then 'Y' else '' end Probation,
		act_executed dscpl_action_date,
		case 
			when num_cycles_non_compliant>0  or 
				num_cycles_non_compliant is NULL or
				(dscpl_action.effective=1 and dscpl_action.dscpl_code!='PR') or 
				getdate()>cast('01/01/'+cast(exam_pass.expiration+1 as varchar(4)) as datetime) or
				optout > '2005-01-01'
			then 'Not Maintained'
			when ISNULL(num_cycles_non_compliant,0)=0 then 'Maintained'
		end CertificationMaintenance,
		IIF(num_cycles_non_compliant is NULL,'Not Required','Required') CertificationMaintenanceRequired,
		exam_pass.exam,
		isnull(convert(varchar(10),isnull(exam_pass.modified,exam_pass.created),20),'') modified,
		initial_cert.date initial_cert_date,
		RTRIM(ISNULL(initial_cert.certificate,'')) certificate,
		IIF(isnull(inactive.UserId,'')='','', IIF(isnull(inactive.end_date, '')='', 'Y', IIF(inactive.end_date>GETDATE(), 'Y', ''))) clinically_inactive,
		IIF(isnull(retired.UserId,'')='','','Y') retired,
		isnull(convert(varchar(10),isnull(retired.start_date,retired.created),20),'') retired_start_date,
		rtrim(isnull(retired.st_val,'')) retired_value,
		ISNULL(exam_eligibility.admissible,'') admissible,
		IIF(dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR','CS'),'',exam_pass.reverification_date) reverification_date,
		IIF(
			(dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR','CS')) or 
			((case when isnull(exam_pass.expiration,0)=0 or RTRIM(ISNULL(initial_cert.certificate,''))='' then 0 when exam_pass.expiration<2014 then DATEDIFF(day,getdate(),cast('07/01/'+cast(exam_pass.expiration as varchar(4)) as datetime))	else DATEDIFF(day,getdate(),cast('01/01/'+cast(exam_pass.expiration+1 as varchar(4)) as datetime)) end)<=0 and ISNULL(durationtype,'')!='L')
		,'N','Y') iscertified,
		exam_pass.id,
		exam_pass.type,
		IIF(top_exam_pass.UserId is null and exam_pass.type!='B',0,1) istop
	FROM exam_pass
		LEFT JOIN (
			SELECT max(exam_pass.durationtype_order) durationtype_order,max(isnull(exam_pass.date,reverification_date)) date,exam_pass.UserId,IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam) exam from exam_pass 
				INNER JOIN (SELECT max(durationtype_order) durationtype_order, UserId,IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam) exam from exam_pass group by UserId, IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam)) a ON a.durationtype_order= exam_pass.durationtype_order AND a.UserId = exam_pass.UserId AND a.exam = IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam) 
			GROUP BY exam_pass.UserId,IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam),exam_pass.durationtype_order
		) top_exam_pass ON top_exam_pass.UserId=exam_pass.UserId and top_exam_pass.exam=IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam) and top_exam_pass.durationtype_order=exam_pass.durationtype_order and top_exam_pass.date=isnull(exam_pass.date,exam_pass.reverification_date)
		
		left join dscpl_action on dscpl_action.UserId=exam_pass.UserId and dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR', 'CS', 'PR')
		left join moc_eligibility on moc_eligibility.UserId=exam_pass.UserId
		left join (select max(certificate) certificate,min(date) date,UserId,exam,IIF(exam_pass.type='B',exam_pass.type,'') isfpd from exam_pass where isnull(expiration,0)!=0 group by UserId,exam,IIF(exam_pass.type='B',exam_pass.type,'')) initial_cert on initial_cert.UserId=exam_pass.UserId and initial_cert.exam=exam_pass.exam and initial_cert.isfpd=IIF(exam_pass.type='B',exam_pass.type,'')
		left join surgeon_st inactive on inactive.UserId=exam_pass.UserId and inactive.status_code='CI'
		left join surgeon_st retired on retired.UserId=exam_pass.UserId and retired.status_code='NP'
		
		LEFT JOIN (
			SELECT MAX(exam_eligibility.admissible) admissible,UserId,IIF(exam_eligibility.type='B',exam_eligibility.type,exam_eligibility.exam) exam FROM exam_eligibility WHERE admissible='Y' AND type NOT IN ('X','P') GROUP BY UserId,IIF(exam_eligibility.type='B',exam_eligibility.type,exam_eligibility.exam)) exam_eligibility 
		ON exam_pass.UserId=exam_eligibility.UserId and IIF(exam_pass.type='B',exam_pass.type,exam_pass.exam)=exam_eligibility.exam 
	where exam_pass.type not in ('X','S','P','I') and exam_pass.exam!='Z'
	UNION
	SELECT
		exam_eligibility.UserId, 
		exam_eligibility.candidate,
		'Initial' CertificationOccurrence,
		'' CertificationDuration,
		IIF(exam_eligibility.exam in ('G','V'),'General Certificate','Subcertificate') CertificateType,
		case exam_eligibility.exam
			when 'G' then 'S' 
			when 'C' then 'SCC' 
			when 'H' then 'HS' 
			when 'O' then 'CGSO'
			when 'P' then 'PdS' 
			when 'V' then 'VascS' 
			when 'Z' then 'HospPalM' 
		end CertificateName,
		null CertificationIssueDate,
		null CertificationExpireDate,
		0 Expired,
		'' Revoked,
		'' Suspended,
		'' Probation,
		null dscpl_action_date,
		'Not Maintained' CertificationMaintenance,
		'Not Required' CertificationMaintenanceRequired,
		exam_eligibility.exam,
		'' modified,
		null initial_cert_date,
		'' certificate,
		'' clinically_inactive,
		'' retired,
		'' retired_start_date,
		''  retired_value,
		ISNULL(exam_eligibility.admissible,'') admissible,
		null reverification_date,
		'N' iscertified,
		0 id,
		'' type,
		1
	from exam_eligibility
		left join exam_pass on exam_pass.UserId=exam_eligibility.UserId and exam_pass.exam=exam_eligibility.exam
	where exam_eligibility.admissible='Y' and exam_eligibility.type not in ('X','P') and exam_pass.UserId is null and exam_eligibility.exam!='Z'
) info