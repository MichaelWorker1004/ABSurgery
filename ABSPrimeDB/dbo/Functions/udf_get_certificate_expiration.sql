CREATE FUNCTION udf_get_certificate_expiration
(
	@candidate VARCHAR(6),
	@exam      VARCHAR(1)
)
RETURNS smallint AS
BEGIN
  	DECLARE @result smallint
	SELECT TOP 1 @result=expiration 
		FROM exam_pass
		WHERE exam=@exam AND candidate=@candidate 
		ORDER BY durationtype_order DESC
  
	RETURN ISNULL(@result,0)
END