CREATE FUNCTION udf_program_check (@examiner char(6),@examinee char(6),@ReturnStatus bit=0)
RETURNS varchar(1000)
AS
begin
DECLARE 
@result varchar(1000),
@msg varchar(100)

set @result=''

if (@ReturnStatus=0)
begin
	if exists(select z.program,z.compyear as e_r,a.compyear as e_e from candidate_program z
		inner join candidate_program a on a.program=z.program and a.candidate=@examinee
		where z.candidate=@examiner)
		begin
			set @result='1'
		end
		else
		begin
			set @result='0'
		end
end
else
begin
	DECLARE program_cursor CURSOR FOR 
	select 'Prg. : '+z.program+', Examiner Year Complited: '+cast(z.compyear as varchar(4))+', Examinee Year Complited: '+cast(a.compyear as varchar(4)) from candidate_program z
		inner join candidate_program a on a.program=z.program and a.candidate=@examinee
		where z.candidate=@examiner
	OPEN program_cursor
	FETCH NEXT FROM program_cursor INTO @msg
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @result=@result+@msg+char(13)+char(10)
		FETCH NEXT FROM program_cursor INTO @msg
	END
	
	CLOSE program_cursor
	DEALLOCATE program_cursor
end

return @result
end

