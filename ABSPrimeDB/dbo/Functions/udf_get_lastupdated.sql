CREATE FUNCTION udf_get_lastupdated (@table_name as varchar(50),@column_name as varchar(50),@table_id as int)
RETURNS varchar(10)
AS
BEGIN
	DECLARE @result varchar(10),@lastupdated smalldatetime
	
	SELECT @lastupdated=lastupdated FROM lastupdated WHERE table_name=@table_name AND column_name=@column_name AND table_id=@table_id;
	
	IF (@lastupdated IS NULL OR year(@lastupdated)= 1900)
	BEGIN
		SET @result=
			(CASE @table_name
				WHEN 'surgeon' 	THEN (SELECT convert(varchar(10),ISNULL(modified,created),120) FROM surgeon WHERE id=@table_id)
				WHEN 'address' 	THEN (SELECT convert(varchar(10),ISNULL(modified,created),120) FROM address WHERE id=@table_id)
				WHEN 'phone' 	THEN (SELECT convert(varchar(10),ISNULL(modified,created),120) FROM phone WHERE id=@table_id)
			END)
	END
	ELSE
	BEGIN
		SET @result=convert(varchar(10),@lastupdated,120)
	END
	
	RETURN @result
END