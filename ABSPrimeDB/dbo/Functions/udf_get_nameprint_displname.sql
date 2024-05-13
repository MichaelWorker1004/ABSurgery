CREATE FUNCTION udf_get_nameprint_displname (@candidate char(6))
RETURNS varchar(100)
AS
BEGIN
	declare @name varchar(100)
	
    	SELECT @name =
      COALESCE(dispname,
      CASE RTRIM(ltrim(ISNULL(first_name,'')))
        WHEN '' THEN ''
        ELSE RTRIM(ltrim(first_name))
      END+
      CASE RTRIM(ltrim(ISNULL(middle_name,'')))
        WHEN '' THEN ''
        ELSE ' '+RTRIM(ltrim(middle_name))
      END       +
      CASE RTRIM(ltrim(ISNULL(last_name,'')))
        WHEN '' THEN ''
        ELSE ' '+RTRIM(ltrim(last_name))
      END       +
      CASE RTRIM(ltrim(ISNULL(suffix,'')))
        WHEN '' THEN ''
        ELSE ' '+RTRIM(ltrim(suffix))
      END)
    	FROM
      	surgeon
    	WHERE
      	surgeon.candidate = @candidate
 
	return isnull(@name,'')
end