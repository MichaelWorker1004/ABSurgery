CREATE FUNCTION udf_get_phone_bytype(@code varchar(6), @type1 varchar(1), @type2 varchar(1))
RETURNS varchar(25)
AS
BEGIN
declare @phone varchar(25)
SELECT @phone=left(replace(replace(replace(replace(replace(number,'-',''),')',''),'(',''),' ',''),'.',''),25)
	from phone p
	where p.code=@code and type1=@type1 and type2=@type2
return isnull(@phone, '')
end