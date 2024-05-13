CREATE FUNCTION [dbo].[udf_get_email] ( @code char(6))
RETURNS varchar(100)
AS
BEGIN
declare @email varchar(100)

SELECT @email=number
	from phone p 
	inner join address a on a.type=p.type1 and a.code=p.code and a.mail='M'
	where p.code=@code and type2='B' and number like '%@%'

if isnull(rtrim(@email),'')=''
begin
	select @email = (select min(number) from phone where code = @code and number like '%@%')
end

return rtrim(ltrim(isnull(@email,'')))
end