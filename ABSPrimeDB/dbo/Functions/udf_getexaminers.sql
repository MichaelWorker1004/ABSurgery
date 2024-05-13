CREATE FUNCTION udf_getexaminers (@timecode tinyint, @day char(1), @year smallint, @area char(1), @exam char(1))
RETURNS varchar(1000)
AS
begin
DECLARE 
@last_name varchar(50),
@result varchar(2000),
@msg varchar(500)
set @result=''

begin
	DECLARE examiner_cursor CURSOR FOR 
		select distinct case isnull(b.program,'') when '' then '' else dbo.udf_get_namelast(a.examiner)+'('+a.examiner+') affiliated with '+case b.program when '9999' then b.note else h.abbname end +'('+b.program+')'+char(13)+char(10) end msg
		from (select board_examiner examiner from  
			exam_teams_planned a
			inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=@exam
			where day=@day and area=@area and year=@year
			UNION
			select assoc_examiner from  
			exam_teams_planned a
			inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=@exam
			where day=@day and area=@area and year=@year) as a
		left outer join candidate_program b on a.examiner=b.candidate
		left join program h on h.number=b.program
	OPEN examiner_cursor
	FETCH NEXT FROM examiner_cursor INTO @msg
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @result=@result+@msg
		FETCH NEXT FROM examiner_cursor INTO @msg
	END
	
	CLOSE examiner_cursor
	DEALLOCATE examiner_cursor
end

return @result
end
