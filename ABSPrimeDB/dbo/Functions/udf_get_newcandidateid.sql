CREATE FUNCTION udf_get_newcandidateid()
RETURNS char(6)
AS
BEGIN
	declare @candidate char(6)
	SELECT 
		@candidate='0'+CAST(cast(candidate as integer)+1 AS VARCHAR)
	FROM 
		(
			select candidate from surgeon where isnumeric(candidate)=1 and cast(candidate as integer)>70000 
					) s1
	WHERE NOT EXISTS (
			SELECT NULL
			FROM	(
				select candidate from surgeon where isnumeric(candidate)=1 and cast(candidate as integer)>70000
			) s2
			WHERE  cast(s2.candidate as integer) = cast(s1.candidate as integer)+1 
		)
	ORDER BY
		candidate 
	DESC
	
	return @candidate
END