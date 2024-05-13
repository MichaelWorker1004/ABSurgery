CREATE view [dbo].[V_RESIDENT] 
as
select 
	resident.created,
	resident.creator,
	resident.modified,
	resident.modifier,
	resident.modifications,
	resident.exam,
	resident.current_year,
	resident.program,
	/*isnull(surgeon.last_name,resident.last_name) last_name,
	isnull(surgeon.first_name,resident.first_name) first_name,
	isnull(surgeon.middle_name,resident.middle_name) middle_name,
	isnull(surgeon.ssn,resident.ssn) ssn,*/
	resident.last_name,
	resident.first_name,
	resident.middle_name,
	resident.ssn,	
	resident.level,
	resident.research,
	resident.comp_year,
	resident.UserId,
	resident.candidate,
	resident.id,
	resident.pd_auth,
	resident.roster_id,
	resident.notes,
	resident.other,
	resident.completed,
	resident.continuing,
	resident.gender,
	resident.birthdate,
	resident.grad_year,
	resident.category,
	resident.rtype,
	resident.uid,
	resident.pwd,
	resident.record_sent,
	resident.fec,
	resident.istaking,
	resident.accommodations,
	resident.alternatesite,
    /*CASE
       	WHEN ISNULL(resident.orderformclinicallevel, 0)>0 THEN resident.orderformclinicallevel
       	WHEN resident.continuing between 3 and 7 then resident.continuing-2	
       	WHEN resident.level BETWEEN 1 AND 5 THEN resident.level
       	ELSE ISNULL(dbo.udf_get_lastlevel(resident.ssn, roster.number+roster.exam), ISNULL(resident.level,0))
    END orderformclinicallevel,*/
    ISNULL(resident.orderformclinicallevel, 0) orderformclinicallevel,
	resident.orderformonly,
	resident.req_met,
	resident.offcycle
from resident
inner join roster on roster.id=resident.roster_id
left outer join surgeon on surgeon.candidate=resident.candidate