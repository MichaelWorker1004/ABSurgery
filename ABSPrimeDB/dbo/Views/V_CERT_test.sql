create view V_CERT_test
as
select 
	surgeon.candidate,
	isnull(info.exam,'') exam,
	isnull(info.type,'') type,
	isnull(info.start_date,'') start_date,
	isnull(info.end_date,'') end_date,
	isnull(info.duration,'') duration,
	isnull(info.Occurrence,'') + case when info.status='Expired' then ' - Expired' when isnull(info.Occurrence,'')='' then info.status else '' end  Occurrence,
	isnull(info.status,'') status, 
	isnull(certificate,'') certificate, 
	isnull(admissible, 'N') admissible,
    isnull(mcodes.Descr,'') revoked,
	case 
		when isnull(cohort,0)=0 then 'Not Required to Participate in MOC' 
		when isnull(num_years_non_compliant,0)!=0 then 'Not Meeting MOC Requirements' 
		else 'Meeting MOC Requirements' 
	end MOCParticipation,
	case isnull(surgeon_st.candidate,'')
		when '' then ''
		else 'true'
	end clinically_inactive	
from surgeon 
left outer join (
		select  case isnull(b.candidate,'') when '' then exam_pass.candidate else b.candidate end candidate,
			case isnull(b.exam,'') when '' then exam_pass.exam else b.exam end exam,
			case isnull(b.type,'') when '' then exam_pass.type else b.type end type,
			case 
				when year(isnull(date,''))= 1900 then '' 
				when isnull(expiration,'')='' then ''
				else isnull(convert(varchar(10),date,120),'') end start_date,
			case expiration when 2099 then 'Indefinite' else cast(expiration as varchar)+'-07-01' end end_date,
			case isnull(expiration,'') when 2099 then 'Lifetime' when '' then '' else 'Time-Limited' end duration,
			case isnull(exam_pass.type,'') when 'O' then 'Initial Certification' when 'W' then 'Initial Certification' when '' then '' else 'Recertification' end Occurrence,
			case 
				when sign(datediff(d,getdate(),cast('07/01/'+cast(expiration as varchar) as datetime))) = -1 then 'Expired' 
				when isnull(expiration,'') = ''  then 'In Examination Process' else 'Active' end status,	
		     isnull(certificate, '') certificate,
		     b.admissible
		from exam_pass 
			full outer join (select candidate,exam,type,admissible from exam_eligibility where admissible='Y'  and type not in ('X','P')) b on b.candidate+b.exam+b.type=exam_pass.candidate+exam_pass.exam+case when exam_pass.type in ('1','2') then 'R' else exam_pass.type end 
		where (isnull(cast(expiration as varchar(4)),'')+isnull(b.candidate,''))!=''  
	) info on surgeon.candidate=info.candidate
left join surgeon_st on surgeon_st.candidate=surgeon.candidate and status_code='CI'
left join dscpl_action on dscpl_action.candidate=surgeon.candidate and dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR', 'CS') 
left join mcodes on dscpl_action.dscpl_code=mcodes.code and mcodes.grp='DC' 
left join moc_eligibility on moc_eligibility.candidate=surgeon.candidate

UNION

select  exam_pass.candidate,
	exam_pass.exam,
	'',
	convert(varchar(10),act_executed,120) start_date,
	'' end_date,
	'' duration,
	'' Occurrence,
	descr  status,
    '' certificate,
    'N' admissible,
    descr,
    '',
    ''
from exam_pass 
inner join dscpl_action on dscpl_action.candidate=exam_pass.candidate and dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR', 'CS')
inner join mcodes on dscpl_action.dscpl_code=mcodes.code and mcodes.grp='DC' 
where exam+type not in ('GW','PW','VW') and type not in ('X','P')