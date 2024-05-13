CREATE FUNCTION udf_bin2dec (@bin as varchar(32))
RETURNS int
AS
BEGIN
	DECLARE @result int,@maxpos as smallint,@cntr as smallint
	set @maxpos=len(@bin)
	set @result=0
	set @cntr=0
	IF (@maxpos>0)
	BEGIN
		WHILE (@cntr<@maxpos)
		BEGIN
			IF (SUBSTRING (@bin, @cntr+1 , 1 )='1')
			BEGIN
				set @result=@result+power(2,@maxpos-@cntr-1)
			END
			SET @cntr=@cntr+1
		END
	END
	ELSE
	BEGIN
		set @result=0
	END
	RETURN @result
END







