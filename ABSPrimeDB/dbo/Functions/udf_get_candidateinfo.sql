CREATE FUNCTION [dbo].[udf_get_candidateinfo] (@candidate char(6)) 
RETURNS varchar(250)
AS
begin

DECLARE 
@result varchar(250),
@certificate varchar(7),
@certificates varchar(50),
@program varchar(7),
@programs varchar(50),
@birthdate varchar(10),
@fsmblicense varchar(100)

set @result=''
set @certificates=''
set @programs=''

select @result=dbo.udf_get_namelastfirst(candidate)+', '+surgeon.candidate+isnull(', ABMS'+rtrim(ABMSCode),'')+case when len(dbo.udf_get_email(candidate))>0 then '; '+dbo.udf_get_email(candidate) else '' end,
@birthdate=case year(isnull(surgeon.birthdate,'')) when 1900 then '' else isnull(convert(varchar(10),surgeon.birthdate,20),'') end,
@fsmblicense=dbo.udf_get_fsmblicense(candidate,', ')
from surgeon 
left outer join ABMSdata on candidate=abscode where candidate=@candidate

DECLARE result_cursor CURSOR FOR 
	select distinct exam+certificate from exam_pass where isnull(certificate,'')!='' and candidate=@candidate

OPEN result_cursor
FETCH NEXT FROM result_cursor INTO
	@certificate

WHILE @@FETCH_STATUS = 0
BEGIN
	set @certificates=@certificates+case when len(@certificates)>0 then ', ' else '' end+@certificate

	FETCH NEXT FROM result_cursor INTO 
		@certificate
	
END
set @result=@result+case when len(@certificates)>0 then '; C:'+@certificates else '' end

CLOSE result_cursor
DEALLOCATE result_cursor

DECLARE result_cursor CURSOR FOR 
	select distinct program+exam from candidate_program where isnull(program,'')!='' and candidate=@candidate and exam!='J'

OPEN result_cursor
FETCH NEXT FROM result_cursor INTO
	@program

WHILE @@FETCH_STATUS = 0
BEGIN
	set @programs=@programs+case when len(@programs)>0 then ', ' else '' end+@program

	FETCH NEXT FROM result_cursor INTO 
		@program
	
END
set @result=@result+case when len(@programs)>0 then '; P:'+@programs else '' end
set @result=@result+case when len(@birthdate)>0 then '; BD:'+@birthdate else '' end
set @result=@result+case when len(rtrim(@fsmblicense))>0 then '; '+@fsmblicense else '' end

CLOSE result_cursor
DEALLOCATE result_cursor

return @result
end