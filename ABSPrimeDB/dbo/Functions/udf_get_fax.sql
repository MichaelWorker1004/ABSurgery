--drop FUNCTION udf_get_fax
CREATE FUNCTION udf_get_fax(@code varchar(6))
RETURNS varchar(10)
AS
BEGIN
declare @phone varchar(10)
SELECT @phone=left(replace(replace(replace(replace(replace(number,'-',''),')',''),'(',''),' ',''),'.',''),10)
	from phone p
	inner join address a on a.type=p.type1 and a.code=p.code and a.mail='M'
	where p.code=@code and type2='F'
return case len(@phone) when 0 then null else @phone end
end

