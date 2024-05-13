CREATE FUNCTION udf_examinee_programs (@examinee char(6))
RETURNS varchar(600)
AS
begin
DECLARE 
@result varchar(600),
@msg varchar(200)
set @result=''

begin
	DECLARE program CURSOR FOR 
		select max(case a.program when '9999' then a.note else b.abbname end +'('+a.program+')'+char(13)+char(10)) msg
		from candidate_program a 
			inner join program b on b.number=a.program
		where a.candidate=@examinee group by a.program order by msg 
	OPEN program
	FETCH NEXT FROM program INTO @msg
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @result=@result+@msg
		FETCH NEXT FROM program INTO @msg
	END
	
	CLOSE program
	DEALLOCATE program
end

return @result
end






