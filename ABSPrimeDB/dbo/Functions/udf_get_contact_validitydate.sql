create FUNCTION udf_get_contact_validitydate(@code varchar(6),@type char(1))
RETURNS varchar(10)
AS
BEGIN
	declare @validitydate varchar(10)
	SELECT @validitydate=dbo.udf_get_lastupdated('phone',case @type when 'V' then 'phone' when 'F' then 'fax' when 'B' then 'email' else '' end,p.id)
		from phone p
		inner join address a on a.type=p.type1 and a.code=p.code and a.mail='M'
		where p.code=@code and type2=@type

	RETURN @validitydate
END