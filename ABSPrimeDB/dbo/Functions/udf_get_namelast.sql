CREATE FUNCTION udf_get_namelast ( @candidate char(6))
RETURNS varchar(100)
AS
BEGIN
	declare @name varchar(100)
	SELECT @name =    
		rtrim(surgeon.[Last Name])
		FROM	v_abms_surgeon as surgeon 
	WHERE surgeon.board_unique_id = @candidate
	
	return isnull(@name,'')
end