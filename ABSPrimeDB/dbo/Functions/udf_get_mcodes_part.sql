CREATE FUNCTION udf_get_mcodes_part (@code varchar(25),@grp varchar(2),@field varchar(8))
RETURNS varchar(1000)
AS
BEGIN
	DECLARE @result varchar(1000),@descr varchar(1000),@attr varchar(1000),@attr2 varchar(1000)

	SELECT @descr=descr,@attr=attr,@attr2=attr2
    	FROM	mcodes
   	WHERE grp=@grp and code=@code

	select @result= case @field
			when 'attr' then @attr
			when 'attr2' then @attr2
			when 'descr' then @descr
		end

	return  rtrim(isnull(@result,''))
END