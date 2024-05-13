CREATE function udf_get_last_oper_receive(
	@candidate char(6),
	@exam_type as varchar(2)
)
RETURNS varchar(100)
AS
BEGIN
	DECLARE @oper_receive as smalldatetime
	select @oper_receive=max(oper_receive) from track 
	where candidate=@candidate and exam+type like '%'+@exam_type+'%' and
		dbo.udf_get_opproc(candidate,examcode)>0 and
		datediff(day,oper_receive,getdate())<3653
	GROUP BY candidate, exam+type 
	
	RETURN case isnull(@oper_receive, '') when '' then 'Not Yet Received' else 'Received ' + convert(varchar(10),@oper_receive, 101) end
END