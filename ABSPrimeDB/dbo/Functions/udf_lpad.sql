CREATE FUNCTION udf_lpad (@srcstr varchar(100), @padlen smallint, @padstr char(1))
RETURNS varchar(100)
AS
begin
DECLARE 
@result varchar(100)

	set @result=(case when (len(@srcstr) < @padlen) then replicate(@padstr, @padlen - len(@srcstr)) + @srcstr else @srcstr end)

return @result
end
