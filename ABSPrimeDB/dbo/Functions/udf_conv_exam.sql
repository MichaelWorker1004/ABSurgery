CREATE FUNCTION udf_conv_exam ( @exam char(1))
RETURNS varchar(100)
AS
BEGIN
declare @examdescription varchar(100)
select @examdescription = (select min(descr) from mcodes where code = @exam and grp='EX')

return isnull(@examdescription,'')
end








