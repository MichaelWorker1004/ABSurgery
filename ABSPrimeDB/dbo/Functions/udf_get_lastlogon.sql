CREATE FUNCTION udf_get_lastlogon (@candidate char(6))
RETURNS datetime
AS
BEGIN
	DECLARE @lastlogon datetime
	SELECT @lastlogon=MAX(created) FROM weblog 
		WHERE candidate=@candidate+'-'+@candidate and left(ip,7)!='192.168' and ip not in ('127.0.0.1','151.197.214.19','216.214.0.59','216.214.0.29','216.214.0.62')
	
	RETURN @lastlogon
END