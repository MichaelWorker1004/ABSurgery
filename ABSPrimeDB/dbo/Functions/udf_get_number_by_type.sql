--drop FUNCTION udf_get_number_by_type
CREATE FUNCTION udf_get_number_by_type(@code varchar(6),@type1 char(1),@type2 char(1))
RETURNS varchar(100)
AS
BEGIN
declare @phone varchar(100)
SELECT @phone=(case @type2 when 'B' then rtrim(number) else rtrim(replace(replace(replace(replace(replace(number,'-',''),')',''),'(',''),' ',''),'.','')) end)
	from phone
	where code=@code and type1=@type1 and type2=@type2
return case len(@phone) when 0 then null else @phone end
end





