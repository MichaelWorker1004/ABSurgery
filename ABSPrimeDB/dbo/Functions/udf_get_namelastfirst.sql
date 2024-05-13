CREATE FUNCTION udf_get_namelastfirst ( @candidate varchar(6))
RETURNS varchar(100)
AS
BEGIN
	declare @result varchar(100)
	
	IF LEN(@candidate)=5
	BEGIN
		SELECT @result = abbname FROM Program where number+exam=@candidate
	END
	ELSE
	BEGIN
		SELECT @result =[Last Name]+', '+[First Name]+' '+[Middle Name]
		FROM	V_ABMS_SURGEON
		WHERE	board_unique_id=@candidate
	END
	
	return isnull(@result,'')
end