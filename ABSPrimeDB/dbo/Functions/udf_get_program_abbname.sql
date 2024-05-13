CREATE FUNCTION udf_get_program_abbname (@numberexam char(5))
RETURNS varchar(100)
AS
BEGIN
	declare @name varchar(100)
	select @name = abbname from program where number+exam=@numberexam
return isnull(@name,'')
end