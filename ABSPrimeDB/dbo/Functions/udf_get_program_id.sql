CREATE FUNCTION udf_get_program_id ()
RETURNS varchar(4)
AS
begin
DECLARE 
@result varchar(4),
@prev int,@next int
begin
	set @next=0
	set @prev=0

	DECLARE id_cursor CURSOR FOR 
		select number from (
		SELECT distinct cast(number as int) number FROM program where isnumeric(number)=1 
		union
		select distinct cast(program as int) number FROM candidate_program where isnumeric(program)=1 and program not in (select number from program)
		) as info
	OPEN id_cursor
	FETCH NEXT FROM id_cursor INTO @next
	WHILE @@FETCH_STATUS = 0
	BEGIN
		if @next<=@prev+1
		begin
			set @prev=@next
		end
		else
		begin
			set @result = cast(@prev+1 as varchar(6))
			set @result = REPLICATE('0',4-len(@result))+@result
			break
		end
		FETCH NEXT FROM id_cursor INTO @next
	END
	CLOSE id_cursor
	DEALLOCATE id_cursor
end

return @result
end



