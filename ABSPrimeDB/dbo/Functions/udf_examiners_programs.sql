CREATE FUNCTION udf_examiners_programs (@timecode tinyint, @day char(1), @year smallint, @area char(1), @exam char(1))
RETURNS varchar(1000)
AS
begin
DECLARE 
@result varchar(1000),
@msg varchar(200)
set @result=''

begin
	DECLARE program CURSOR FOR 
		select max(isnull(c.abbname,'') +'('+isnull(c.number,'')+')'+char(13)+char(10)) msg
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
			left join program c on c.number=b.program 
		group by c.number 
		order by msg
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
