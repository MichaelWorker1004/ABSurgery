CREATE function udf_get_reflet(@candidate char(6),@exam_type varchar(6))

RETURNS TABLE
AS

RETURN (
	select 
		appreflet.*,
		case isnull(let_rcvd, '') when '' then 'Not Yet Received' else 'Received from '+isnull(official, '')+', '+isnull(mcodes.descr, '')+' ('+convert(varchar(10),let_rcvd, 101)+')' end reflet_receive,
		case isnull(let_sent, '') when '' then 'Not Yet Sent' else 'Request sent to '+isnull(official, '')+', '+isnull(mcodes.descr, '')+' ('+convert(varchar(10),let_sent, 101)+')' end reflet_sent,
		isnull(official, '') as reflet_sendto,
		isnull(mcodes.descr, '') as reflet_vp,
		case when datediff(dd,isnull(let_approve,getdate()),getdate())<1827 then 0 else 1 end reflet_expired,
		case isnull(let_approve, '') when '' then 'Not Yet Approved ' else 'Approved on '+convert(varchar(10),let_rcvd, 101) end reflet_approve
	from appreflet 
         left join mcodes on mcodes.code=appreflet.let_type and mcodes.grp='VP' 	
	where 
		isnull(exam_type,'')=@exam_type AND candidate = @candidate
)