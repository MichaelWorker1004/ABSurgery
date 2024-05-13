CREATE FUNCTION udf_isconflictgroup (@examinee char(6), @timecode tinyint, @day char(1), @year smallint, @area char(1),@exam exam,@type char(1))
RETURNS varchar(1000)
AS
begin
DECLARE 
@result varchar(1000),
@msg varchar(300)
set @result=''

begin
	DECLARE conflict_cursor CURSOR FOR 
		select distinct 
	
			case isnull(f.year,0) when 0 then '' else rtrim(z.last_name)+'('+a.examiner+') manual override '+cast(f.year as varchar(4))+', area '+cast(f.area as varchar(1))+' '+f.reason+char(13)+char(10) end+
			case isnull(e.year,0) when 0 then '' else rtrim(z.last_name)+'('+a.examiner+') examined in '+cast(e.year as varchar(4))+char(13)+char(10) end +	
			case isnull(l.program,'') when '' then '' else rtrim(z.last_name)+'('+a.examiner+') affiliated with the same program/institution: '+case l.program when '9999' then k.note else m.abbname end +'('+l.program+')'+char(13)+char(10) end msg
	
		from (select board_examiner examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam UNION select assoc_examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam) as a
		
			inner join surgeon z on z.candidate=a.examiner 
			
			left join exam_exception f on f.examiner in (a.examiner,rtrim(z.last_name))  and f.candidate=@examinee
	
			left join exam_score d on d.examiner=a.examiner
			left join exam e on e.id=d.exam_id and e.candidate=@examinee
	
	
			left join candidate_program k on a.examiner=k.candidate
			left join candidate_program l on k.program=l.program and l.candidate=@examinee
			left join program m on m.number=l.program
	
		where (f.examiner is not null and e.id is null and l.program is null) or
			(f.examiner is null and e.id is not null and l.program is null) or
			(f.examiner is null and e.id is null and l.program is not null)

		UNION
		
		select distinct 
			case isnull(k.program,'') when '' then '' else rtrim(z.last_name)+'('+a.examiner+') affiliated with the same program/institution (GME): '+case k.program when '9999' then k.note else m.abbname end +' ('+k.program+')'+char(13)+char(10) end msg
		
		from (select board_examiner examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam UNION select assoc_examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam) as a	
			inner join surgeon z on z.candidate=a.examiner 
	
			inner join candidate_program k on a.examiner=k.candidate
			inner join cmp_education on left(right(prg_name,5),4)=k.program 
			inner join program m on m.number=k.program 
		where CHARINDEX ('[',prg_name)>0 and cmp_education.candidate=@examinee 		
		
		UNION

		select distinct 
			case isnull(l.q212,'') when '' then '' else rtrim(z.last_name)+'('+a.examiner+') TRAINING in the same program/institution: '+m.[Organization Name]+'('+cast(m.[Organization Id] as varchar)+')'+char(13)+char(10) end msg
	
		from (select board_examiner examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam UNION select assoc_examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam) as a
		
			inner join surgeon z on z.candidate=a.examiner 
	
			inner join AppReplyCardsTraining k on a.examiner=k.candidate and CHARINDEX ('[',k.q212)>0
			inner join AppReplyCardsTraining l on l.candidate=@examinee and left(right(rtrim(k.q212),5),4)=left(right(rtrim(l.q212),5),4) and CHARINDEX ('[',l.q212)>0
			left join jcaho m on cast(m.[Organization ID] as varchar)=left(right(rtrim(l.q212),5),4)

		UNION	

		select distinct 
			case isnull(l.q212,'') when '' then '' else rtrim(z.last_name)+'('+a.examiner+') PRACTICE in the same program/institution: '+m.[Organization Name]+'('+cast(m.[Organization Id] as varchar)+')'+char(13)+char(10) end msg
	
		from (select board_examiner examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam UNION select assoc_examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam) as a
		
			inner join surgeon z on z.candidate=a.examiner 
	
			inner join AppReplyCardsPractice k on a.examiner=k.candidate and CHARINDEX ('[',k.q212)>0
			inner join AppReplyCardsPractice l on l.candidate=@examinee and left(right(rtrim(k.q212),5),4)=left(right(rtrim(l.q212),5),4) and CHARINDEX ('[',l.q212)>0
			left join jcaho m on cast(m.[Organization ID] as varchar)=left(right(rtrim(l.q212),5),4)

		UNION	

		select distinct rtrim(z.last_name)+'('+a.examiner+') affiliated with '+case p.number when '9999' then p.note else p.abbname end +'('+p.number+')'+char(13)+char(10) msg
		from (select board_examiner examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam UNION select assoc_examiner from exam_teams_planned a inner join exam_timeslot b on a.col=b.col  and timecode=@timecode and b.exam=a.exam where day=@day and area=@area and year=@year and type=@type and a.exam=@exam) as a
			inner join surgeon z on z.candidate=a.examiner 
	
			left join program x on x.number in (select program from candidate_program where candidate=a.examiner) and isnull(x.rrc_id,'')!=''  			left join program y on y.number in (select program from candidate_program where candidate=@examinee) and isnull(y.rrc_id,'')!=''  	
			left join relations b on x.ProgramId in (b.childid,b.parentid) and b.date_deleted is null 			left join relations c on y.ProgramId in (c.childid,c.parentid) and c.date_deleted is null and c.parentid=b.parentid 	
			inner join program p on c.parentid=p.ProgramId
		order by msg

	OPEN conflict_cursor
	FETCH NEXT FROM conflict_cursor INTO @msg
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @result=@result+@msg
		FETCH NEXT FROM conflict_cursor INTO @msg
	END
	
	CLOSE conflict_cursor
	DEALLOCATE conflict_cursor
end

return @result
end
