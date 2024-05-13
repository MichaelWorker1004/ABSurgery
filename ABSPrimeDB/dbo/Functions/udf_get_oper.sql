CREATE function  dbo.udf_get_oper(@candidate char(6),@exam varchar(1))

RETURNS TABLE
AS

RETURN (
	select 
		case isnull(max_oper_receive, '') when '' then '' else 'Received ' + convert(varchar(10),max_oper_receive, 101) end as max_oper_receive,
		case isnull(track.oper_receive, '') when '' then 'Not Yet Received' else 'Received ' + convert(varchar(10),track.oper_receive, 101) end as oper_receive, 
		case isnull(track.oper_approve, '') when '' then 'Not Yet Approved' else 'Approved ' + convert(varchar(10),track.oper_approve, 101) end as oper_approve,
		case isnull(app_response.last_updt, '') when '' then '' else 'Last Updated: ' + convert(varchar(10),app_response.last_updt, 101) end as oper_last_updt,
		ISNULL(dbo.udf_get_certificate_number(@candidate,@exam),'') is_cert
	from surgeon 
	inner join mcodes on mcodes.grp='AY' and code='T' 
	left join track  on track.candidate=@candidate and track.year=mcodes.attr and track.exam=@exam and track.type='R'
	left join app_response on app_response.candidate=@candidate and app_response.exam_type like @exam+'R'+'%' and app_response.app_id=1
	left join
		(select max(oper_receive) max_oper_receive,candidate from track
			inner join mcodes on mcodes.grp='AY' and code='T'  
			where candidate=@candidate and exam like '%'+@exam+'%' and
			(
				(dbo.udf_get_opproc(candidate,examcode)>0 and (cast(mcodes.attr as int)-year(oper_receive))<11)
				OR 
				(cast(mcodes.attr as int)-year(oper_approve))<11
			)
			GROUP BY candidate, exam
		) max_track on 1=1
	where surgeon.candidate=@candidate
)