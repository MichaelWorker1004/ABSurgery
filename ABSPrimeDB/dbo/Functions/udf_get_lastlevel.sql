CREATE FUNCTION udf_get_lastlevel (@ssn varchar(9),@program varchar(5))
RETURNS varchar(2)
AS
BEGIN
	DECLARE @lastlevel varchar(2)

	SELECT TOP 1
		@lastlevel=REPLACE(mcodes.code, 'L', '') 
    	FROM	resident
		INNER JOIN mcodes ON mcodes.attr=CAST(resident.level AS VARCHAR) AND mcodes.grp='R'+CAST(resident.rtype AS VARCHAR)
		INNER JOIN roster ON roster.id=resident.roster_id
   	WHERE level<8 
   		AND RTRIM(ISNULL(ssn, ''))!=''
   		AND RTRIM(ssn)=@ssn 
   		AND roster.number+roster.exam=@program
	ORDER BY roster.current_year DESC

	RETURN @lastlevel
END