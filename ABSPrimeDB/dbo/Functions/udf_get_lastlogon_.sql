CREATE FUNCTION udf_get_lastlogon_ (@candidate char(6))
RETURNS datetime
AS
BEGIN
	DECLARE @lastlogon datetime
	SELECT @lastlogon=MAX(created) FROM 
		(select created,candidate from weblog UNION select created,candidate from weblog_old)
		weblog 
		WHERE LEFT(candidate,6)=@candidate	
	
	RETURN @lastlogon
END