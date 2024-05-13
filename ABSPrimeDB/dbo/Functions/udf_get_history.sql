CREATE FUNCTION udf_get_history (@candidate char(6))
RETURNS varchar(8000)
AS
begin
DECLARE 
@result varchar(8000),
@msg varchar(1000),
@date datetime

set @result=''
DECLARE history_cursor CURSOR FOR 
select	date=(case isnull(date,'') when '' then rply_sent else date end),
	msg=	isnull(convert(varchar(10),(case isnull(date,'') when '' then rply_sent else date end),101),'')+(case isnull(case isnull(date,'') when '' then rply_sent else date end ,'') when '' then '' else '   ' end)+rtrim(case exam when 'G' then 'General' when 'P' then 'Pediatric' when 'V' then 'Vascular' when 'C' then 'Critical Care' when 'H' then 'Hand' else exam end)+
		' '+rtrim(case type when 'W' then 'Qualify' when 'O' then 'Certify' when 'R' then 'Recert' when 'I' then 'Readm. ITE' when 'S' then 'Readm. SESAP' else type end)+
		' Exam'+rtrim(case isnull(result,'') when '' then 
			rtrim(case status when 'R' then ' - Registered' when 'C' then ' - Completed' when 'D' then ' - Deferred' when 'N' then ' - Cancelled' when 'P' then ' - Postponed' when 'S' then ' - No Show' else ' - Not Registered' end) 
			else
			rtrim(case result when 'P' then ' - PASS' when 'F' then ' - FAIL' else '' end)
			end)+
		rtrim(case isnull(note,'') when '' then '' else +char(13)+char(10)+replicate('*',40)+char(13)+char(10)+note+char(13)+char(10)+replicate('*',40)+char(13) end)
from exam where candidate=@candidate
union
select	date=	cast('06/01/'+cast(c.compyear as varchar(4)) as datetime),
	msg=	rtrim('06/01/'+cast(c.compyear as varchar(4))+'   '+'Program'+' '+p.abbname+' - '+
		rtrim(case c.exam when 'G' then 'General' when 'P' then 'Pediatric' when 'V' then 'Vascular' when 'C' then 'Critical Care' when 'H' then 'Hand' else c.exam end)+
		' Program '+rtrim(case isnull(c.remedial,'') when '' then 'Completed' else 'Additional' end))
from candidate_program c, program p where candidate=@candidate and p.number=c.program and p.exam=c.exam

OPEN history_cursor
FETCH NEXT FROM history_cursor INTO
 	@date,
	@msg

WHILE @@FETCH_STATUS = 0
BEGIN
	set @result=@result+@msg+char(13)+char(10)
	FETCH NEXT FROM history_cursor INTO 
	 	@date,
		@msg
END

CLOSE history_cursor
DEALLOCATE history_cursor

return @result
end