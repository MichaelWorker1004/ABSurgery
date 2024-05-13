CREATE FUNCTION udf_get_countfirstyearresidents ( @program varchar(10))
RETURNS varchar(10)
AS
BEGIN
declare @rescount varchar(10)
select @rescount = (select count(*) from resident where program=@program and level=1 and exam='g')

return @rescount
end