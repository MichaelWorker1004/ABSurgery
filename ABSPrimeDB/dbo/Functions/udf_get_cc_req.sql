CREATE function udf_get_cc_req(@candidate char(6))

RETURNS TABLE
AS

RETURN (
	
	SELECT *,
		IIF(cc_status IN (1,5) AND num_cycles_non_compliant=0 AND ISNULL(next_reporting_due_dt,getdate())>=getdate(),'Y','N') cc_moc_req_met
	FROM (
		select 
			surgeon.candidate,
			isnull(num_cycles_non_compliant,0) num_cycles_non_compliant, 
			case 
				when isnull(cohort,0)=0 then 'Not Required to Participate in CC' 
				when isnull(num_cycles_non_compliant,0)!=0 or optout > '2005-01-01' then 'Not Meeting CC Requirements'  
				else 'Meeting CC Requirements' 
			end MOCParticipation, 
			case 
								when isnull(continuous.cnt_continuous,0)>0 AND continuous.expiration>=year(getdate()) then 7 								when exam_pass.expiration<year(getdate()) AND isnull(cohort,0)=0 then 6 				when exam_pass.expiration<year(getdate()) then 4 				when isnull(cohort,0)=0 and exam_pass.expiration is null then 0 				when isnull(cohort,0)=0 then 5 				when isnull(num_cycles_non_compliant,0)!=0 or optout > '2005-01-01' then 3 				else 1 			end cc_status,
			case 
				when oridginallp.candidate IS NOT NULL then 4
				when YEAR(oper_g.certificate_expiration)>=YEAR(getdate()) OR 
					YEAR(oper_v.certificate_expiration)>=YEAR(getdate()) OR 
					YEAR(oper_p.certificate_expiration)>=YEAR(getdate()) OR 
					YEAR(oper_c.certificate_expiration)>=YEAR(getdate()) OR 
					YEAR(oper_o.certificate_expiration)>=YEAR(getdate()) OR 
					YEAR(oper_h.certificate_expiration)>=YEAR(getdate()) then 2
				when ISNULL(exam_pass.expiration,0)<year(getdate()) then 4
				else 2
			end cc_refletinstancesreq,			
			(select count(*) from AppRefLet where candidate=@candidate and exam_type='') cc_refletinstances,
		 	isnull((SELECT case when 
					adversecount >0 
				then '35_'+cast(sec_order as varchar)+','
				else ''
				end AS 'data()'
			        FROM AppRefLet
			    	WHERE candidate=@candidate and exam_type=''
			        FOR XML PATH('')),'') cc_refletadverse,
			isnull((SELECT IIF(let_approve IS NULL,cast(sec_order as varchar)+',','') AS 'data()'
				   FROM AppRefLet
				WHERE candidate=@candidate and exam_type=''
				   FOR XML PATH('')),'') cc_refletnotapproved,
			(SELECT SUM(reflet_expired) FROM dbo.udf_get_reflet(@candidate,'')) cc_refletexpired,
			(SELECT count(*) FROM dbo.udf_get_reflet(@candidate,'') WHERE reflet_receive like 'Received%' and reflet_expired=0) cc_refletreceivedinstances,
			(SELECT count(*) FROM dbo.udf_get_reflet(@candidate,'') WHERE reflet_sent like 'Request%' and reflet_expired=0) cc_refletsentinstances,
			isnull(current_cycle,0) current_cycle, 
			isnull(DATENAME(month, current_cycle_due)+' '+DATENAME(d, current_cycle_due)+', '+DATENAME(yyyy,current_cycle_due),'No information on record') current_cycle_due, 
			isnull('March 1, '+cast(year(current_cycle_due+1) as varchar),'No information on record') current_reporting_due, 
			isnull(DATENAME(month, next_reporting_due)+' '+DATENAME(d, next_reporting_due)+', '+DATENAME(yyyy,next_reporting_due),'No information on record') next_reporting_due,
			next_reporting_due next_reporting_due_dt, 
			isnull(case mcodes.code when 'G' then 'General ' else '' end + mcodes.descr+' '+case substring(ISNULL(moc_eligibility.examcode,moc_eligibility.cmoc_examcode),6,1) when 'R' then 'Recertifying' else 'Certifying' end,'No information on record') +' Examination' seniority_examcode, 
			isnull(DATENAME(month, ISNULL(seniority_date,cmoc_seniority_date))+' '+DATENAME(d, ISNULL(seniority_date,cmoc_seniority_date))+', '+DATENAME(yyyy,ISNULL(seniority_date,cmoc_seniority_date)),'No information on record') seniority_date, 	
			isnull(DATENAME(month, optout)+' '+DATENAME(d, optout)+', '+DATENAME(yyyy,optout),'No information on record') optout, 
			current_year, 
			cohort, 
			
			CASE WHEN exam_pass.expiration<dbo.udf_get_academic_year('M') THEN 'Y' ELSE '' END cc_lapsed,
			ISNULL(dbo.udf_get_academic_year('M')-YEAR(lp.start_date)+1,0) cc_lapsed_year,
		
			IIF(isnull(continuous.cnt_continuous,0)>0 AND isnull(num_cycles_non_compliant,0)=0,125,150) cc_cme_req, 
			IIF(isnull(continuous.cnt_continuous,0)>0 AND isnull(num_cycles_non_compliant,0)=0,0,50) cc_cme_req_sa, 
			
			oper_g.is_cert iscert_g,
			case oper_g.oper_last_updt when NULL then '' else 'Last Updated: ' + convert(varchar(10),oper_g.oper_last_updt, 101) end as oper_last_updt_g,
			oper_g.isvalid isvalid_g,
			oper_g.certificate_expiration certificate_expiration_g,
			oper_g.CertificationExpireDate CertificationExpireDate_g,
			oper_g.CertificationDuration CertificationDuration_g,
			oper_g.lp_enddate lp_enddate_g,
			oper_g.islf islf_g,
			
			oper_v.is_cert iscert_v,
			case oper_v.oper_last_updt when NULL then '' else 'Last Updated: ' + convert(varchar(10),oper_v.oper_last_updt, 101) end as oper_last_updt_v,
			oper_v.isvalid isvalid_v,
			oper_v.certificate_expiration certificate_expiration_v,
			oper_v.CertificationExpireDate CertificationExpireDate_v,
			oper_v.CertificationDuration CertificationDuration_v,
			oper_v.lp_enddate lp_enddate_v,	
			oper_v.islf islf_v,		
			
			oper_p.is_cert iscert_p,
			case oper_p.oper_last_updt when NULL then '' else 'Last Updated: ' + convert(varchar(10),oper_p.oper_last_updt, 101) end as oper_last_updt_p,
			oper_p.isvalid isvalid_p,
			oper_p.certificate_expiration certificate_expiration_p,
			oper_p.CertificationExpireDate CertificationExpireDate_p,
			oper_p.CertificationDuration CertificationDuration_p,
			oper_p.lp_enddate lp_enddate_p,	
			oper_p.islf islf_p,		
			
			oper_c.is_cert iscert_c,
			case oper_c.oper_last_updt when NULL then '' else 'Last Updated: ' + convert(varchar(10),oper_c.oper_last_updt, 101) end as oper_last_updt_c,
			oper_c.isvalid isvalid_c,
			oper_c.certificate_expiration certificate_expiration_c,
			oper_c.CertificationExpireDate CertificationExpireDate_c,
			oper_c.CertificationDuration CertificationDuration_c,
			oper_c.lp_enddate lp_enddate_c,	
			oper_c.islf islf_c,		
			
			oper_o.is_cert iscert_o,
			case oper_o.oper_last_updt when NULL then '' else 'Last Updated: ' + convert(varchar(10),oper_o.oper_last_updt, 101) end as oper_last_updt_o,
			oper_o.isvalid isvalid_o,
			oper_o.certificate_expiration certificate_expiration_o,
			oper_o.CertificationExpireDate CertificationExpireDate_o,
			oper_o.CertificationDuration CertificationDuration_o,
			oper_o.lp_enddate lp_enddate_o,		
			oper_o.islf islf_o,	
			
			oper_h.certificate_expiration certificate_expiration_h,
			oper_h.CertificationExpireDate CertificationExpireDate_h,
			oper_h.CertificationDuration CertificationDuration_h,
			oper_h.lp_enddate lp_enddate_h,	
			oper_h.islf islf_h,
			
			isanyopexvalid=oper_g.isvalid+oper_v.isvalid+oper_p.isvalid+oper_c.isvalid			
		from surgeon 
			LEFT OUTER JOIN moc_eligibility ON moc_eligibility.candidate=surgeon.candidate 
			
						LEFT OUTER JOIN (
				SELECT  exam_pass.candidate, MAX(expiration) expiration 
				FROM exam_pass 
					INNER JOIN (SELECT max(durationtype_order) durationtype_order,candidate FROM exam_pass GROUP BY candidate) durationtype_order ON durationtype_order.durationtype_order=exam_pass.durationtype_order and durationtype_order.candidate=exam_pass.candidate
				WHERE expiration is not null GROUP BY exam_pass.candidate
			) AS exam_pass ON exam_pass.candidate=surgeon.candidate
			
			LEFT OUTER JOIN (SELECT count(durationtype) cnt_continuous,MAX(expiration) expiration, candidate FROM exam_pass WHERE durationtype='C' GROUP BY candidate) continuous ON continuous.candidate=surgeon.candidate
			
			OUTER APPLY dbo.udf_get_opex(surgeon.candidate,'G') oper_g 
			OUTER APPLY dbo.udf_get_opex(surgeon.candidate,'V') oper_v 
			OUTER APPLY dbo.udf_get_opex(surgeon.candidate,'P') oper_p 
			OUTER APPLY dbo.udf_get_opex(surgeon.candidate,'C') oper_c 
			OUTER APPLY dbo.udf_get_opex(surgeon.candidate,'O') oper_o 
			OUTER APPLY dbo.udf_get_opex(surgeon.candidate,'H') oper_h 

			LEFT JOIN mcodes on mcodes.code=substring(ISNULL(moc_eligibility.examcode,moc_eligibility.cmoc_examcode),5,1) and mcodes.grp='EX' 
						LEFT JOIN (select min(start_date) start_date,candidate from surgeon_st where LEFT(status_code,2)='LP' AND end_date>getdate() group by candidate) lp ON lp.candidate=surgeon.candidate
			LEFT JOIN (select min(start_date) start_date,candidate from surgeon_st where LEFT(status_code,2)='LP' AND LEN(status_code)<4 AND end_date>getdate() group by candidate) oridginallp ON oridginallp.candidate=surgeon.candidate
		where 
			surgeon.candidate = @candidate
	) INFO	
)