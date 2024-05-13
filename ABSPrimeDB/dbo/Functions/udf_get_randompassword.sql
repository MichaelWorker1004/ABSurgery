CREATE FUNCTION udf_get_randompassword(@code as uniqueidentifier)
RETURNS varchar(8)
AS
BEGIN
declare @password as varchar(8)


SELECT @password=password from passwords where rownum = (select ABS(CAST(CAST(@code AS BINARY(6)) AS INT) % 10000))
return @password
end