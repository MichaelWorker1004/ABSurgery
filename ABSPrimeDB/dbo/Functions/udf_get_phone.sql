CREATE FUNCTION udf_get_phone(@code varchar(6))
RETURNS varchar(10)
AS
BEGIN
	DECLARE @phone varchar(10)
	SELECT @phone=left(replace(replace(replace(replace(replace(number,'-',''),')',''),'(',''),' ',''),'.',''),10)
	FROM phone p
		inner join address a on a.type=p.type1 and a.code=p.code and a.mail='M'
	WHERE p.code=@code AND type2='V'
		
	return case len(@phone) when 0 then null else @phone end
END