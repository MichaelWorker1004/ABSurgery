CREATE FUNCTION udf_convert_datetomidnight ( @date1 datetime)
RETURNS datetime
AS
BEGIN
declare @dateconv varchar(20)
set @dateconv=convert(varchar(20),@date1,101)


return cast (@dateconv as datetime)
end
