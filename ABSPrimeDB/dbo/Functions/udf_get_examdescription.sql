CREATE FUNCTION udf_get_examdescription ( @examcode varchar(7))
RETURNS varchar(100)
AS
BEGIN
declare @examdescription varchar(100)
select @examdescription = (select min(descr) from mcodes where code = @examcode and grp='EC')

return isnull(@examdescription,'')
end



