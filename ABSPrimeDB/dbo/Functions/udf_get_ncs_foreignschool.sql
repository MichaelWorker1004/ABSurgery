CREATE FUNCTION dbo.udf_get_ncs_foreignschool(
	@candidate as char(6),
	@exam_type as varchar(8),
	@app_id as tinyint
)
RETURNS varchar(max)
AS
BEGIN
	DECLARE @x xml,@result varchar(max)
	SELECT @x = response FROM app_response WHERE candidate=@candidate and exam_type=@exam_type and app_id=@app_id
	SELECT @result=@x.query('//q320[@iid="13"]').value('.', 'varchar(max)')

	RETURN ISNULL(@result,'')
END