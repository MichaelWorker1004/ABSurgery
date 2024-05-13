--drop proc cand_prg_qe 
CREATE proc cand_prg_qe 
	@candidate char(6),
	@doc text
as 
begin
	DECLARE @idoc int,@count int

	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	INSERT candidate_program (candidate,exam,compyear,program)  
	SELECT 	@candidate,
		'T',
		max(year(cast(compyear as smalldatetime))),
		left(right(program,5),4)
	FROM OPENXML (@idoc, '//answer[q19[@iid="30"]]',10) with 
	(	
		compyear char(4) 'q20[@iid="31"]',
		program varchar(100) 'q18[@iid="35"]'
	)
	group by program

	EXEC sp_xml_removedocument @idoc
end