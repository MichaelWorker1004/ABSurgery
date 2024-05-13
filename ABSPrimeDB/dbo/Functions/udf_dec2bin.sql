CREATE FUNCTION udf_dec2bin (@dec as int)
RETURNS varchar(50)
AS
BEGIN
	DECLARE @result varchar(50),@maxpos as smallint,@cntr as smallint,@temp as int
	IF (@dec>0)
		begin
		set @cntr=0
		set @result='1'
		set @maxpos=cast(ceiling(Log(@dec+0.001)/Log(2)) as smallint)  
		set @temp=power(2,@maxpos-1)
		WHILE (@cntr < (@maxpos-1))
		BEGIN
			set @result = @result + cast(floor((@dec-@temp)/power(2,(@maxpos-@cntr-2))) as varchar(1))
			if (right(@result,1)='1')
			BEGIN
				set @temp=@temp+power(2,(@maxpos-@cntr-2))
			END
			set @cntr=@cntr+1
		END
	END
	ELSE
	BEGIN
		set @result='0'
	END
	RETURN @result
END


