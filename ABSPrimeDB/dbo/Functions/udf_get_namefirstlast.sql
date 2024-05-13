CREATE FUNCTION udf_get_namefirstlast ( @candidate char(6))
RETURNS varchar(100)
AS
BEGIN
declare @result varchar(100)

	SELECT @result =[First Name]+' '+ CASE isnull([Middle Name], '') WHEN '' THEN '' ELSE left([Middle Name],1)+'. ' END +[Last Name]
	FROM	V_ABMS_SURGEON
	WHERE	board_unique_id=@candidate

return isnull(@result,'')
end