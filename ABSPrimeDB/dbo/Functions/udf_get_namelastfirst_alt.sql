CREATE FUNCTION udf_get_namelastfirst_alt ( @candidate varchar(6))
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
	select @result = (SELECT [Last Name]+', '+[First Name]+' '+left([Middle Name],1) 
	FROM	V_ABMS_SURGEON
	WHERE	board_unique_id=@candidate)
END

return isnull(@result,'')
end