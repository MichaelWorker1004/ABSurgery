CREATE FUNCTION udf_get_nameprint ( @candidate char(6))
RETURNS varchar(100)
AS
BEGIN
	DECLARE @name varchar(100)
	SELECT @name =  
		surgeon.[First Name] + ' ' + (CASE Len(surgeon.[Middle Name]) WHEN 0 THEN '' ELSE surgeon.[Middle Name] + ' ' END) + surgeon.[Last Name]+ (CASE Len(surgeon.[Suffix]) WHEN 0 THEN '' ELSE ', ' + surgeon.[Suffix] + '' END)+
		(CASE (surgeon.[Degree]) WHEN 'MD' THEN ', M.D.' WHEN 'MBBS' THEN ', M.B.B.S.' WHEN 'DO' THEN ', D.O.'  when 'MBChB' then ', M.B.Ch.B'  ELSE surgeon.[Degree] END) 
	FROM	v_abms_surgeon as surgeon 
	WHERE surgeon.board_unique_id = @candidate
	
	RETURN isnull(@name,'')
END