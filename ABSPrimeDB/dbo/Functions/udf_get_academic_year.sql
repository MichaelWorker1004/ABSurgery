CREATE FUNCTION udf_get_academic_year ( @code char(1))
RETURNS char(4)
AS
BEGIN
    return (select attr from mcodes where grp='AY' and code=@code)
END