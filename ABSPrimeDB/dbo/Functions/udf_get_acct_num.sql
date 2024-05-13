CREATE FUNCTION udf_get_acct_num (@candidate char(6))
RETURNS char(13)
AS
BEGIN
	DECLARE @result char(13)
	SELECT @result=acct_num from surgeon where candidate=@candidate
	RETURN @result
END