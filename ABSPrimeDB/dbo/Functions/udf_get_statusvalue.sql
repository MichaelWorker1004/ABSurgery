CREATE FUNCTION udf_get_statusvalue (@candidate char(6),@status_code varchar(50))
RETURNS varchar(max)
AS
BEGIN
	DECLARE @result varchar(max)
	SELECT @result=ISNULL(st_val,'') FROM surgeon_st WHERE candidate=@candidate AND status_code=@status_code;
	RETURN ISNULL(@result ,'')
END