CREATE FUNCTION udf_conv_type ( @type char(1))
RETURNS varchar(100)
AS
BEGIN
declare @examdescription varchar(100)
select @examdescription = (select min(descr) from mcodes where code = @type and grp='TY')

return isnull(@examdescription,'')
end


