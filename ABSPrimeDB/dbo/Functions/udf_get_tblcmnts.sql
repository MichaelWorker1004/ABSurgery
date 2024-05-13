CREATE FUNCTION udf_get_tblcmnts ( @tblname varchar(50))
RETURNS varchar(100)
AS
BEGIN
declare @name varchar(100)
select @name = (SELECT     
	rtrim(descr) AS name
FROM	mcodes
WHERE GRP='TP' and CODE=@tblname)

return isnull(@name,'')
end





