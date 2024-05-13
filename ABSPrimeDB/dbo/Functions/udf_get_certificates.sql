CREATE FUNCTION [dbo].[udf_get_certificates] (@candidate char(6)) 
RETURNS varchar(50)
AS
begin
DECLARE 
@result varchar(50),
@certificate char(7)


set @result=''
set @certificate=''

DECLARE result_cursor CURSOR FOR 
	select distinct certificate+exam from exam_pass where isnull(certificate,'')!='' and candidate=@candidate

OPEN result_cursor
FETCH NEXT FROM result_cursor INTO
	@certificate

WHILE @@FETCH_STATUS = 0
BEGIN
	set @result=@result+case when len(@result)>0 then ', ' else '' end+@certificate

	FETCH NEXT FROM result_cursor INTO 
		@certificate
	
END

CLOSE result_cursor
DEALLOCATE result_cursor
return @result
end