CREATE FUNCTION udf_get_lastlogon__ (@candidate char(6))
RETURNS datetime
AS
BEGIN
	DECLARE @lastlogon datetime
	SELECT @lastlogon=MAX(created) FROM weblog 
		WHERE candidate=@candidate+'-'+@candidate and left(ip,11)!='192.168.111' and ip!='127.0.0.1'
	
	RETURN @lastlogon
END