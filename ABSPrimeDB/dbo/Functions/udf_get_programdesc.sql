CREATE FUNCTION udf_get_programdesc (@program char(4))
RETURNS varchar(150)
AS
begin
DECLARE 
@result varchar(150)
BEGIN
	SELECT @result = 
	(
		SELECT
		 max(p.abbname + case isnull(a.city, '') when '' then '' else ', ' + a.city + ', ' + a.state end) pname
		FROM
		 program p left outer JOIN
		 address a ON p.number+'G'=a.code and a.status = 'P'
		WHERE 
		 p.number = @program 
		GROUP BY
		 p.number
	)
RETURN isnull(@result, '')
END
END

