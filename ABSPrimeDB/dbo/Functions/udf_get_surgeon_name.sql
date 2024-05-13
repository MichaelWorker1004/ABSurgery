
CREATE FUNCTION udf_get_surgeon_name ( @candidate char(6))
RETURNS varchar(100)
AS
BEGIN
declare @result varchar(100)
select @result = (SELECT [Last Name]+', '+[First Name]+' '+left([Middle Name],1) 
FROM	V_ABMS_SURGEON
WHERE	board_unique_id=@candidate)
return isnull(@result,'')
end

