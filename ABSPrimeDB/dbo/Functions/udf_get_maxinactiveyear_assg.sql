
--drop FUNCTION udf_get_maxinactiveyear_assg
CREATE FUNCTION udf_get_maxinactiveyear_assg(@code varchar(6))
RETURNS int
AS
BEGIN
DECLARE @result int
select @result=max(year) from exam where 
candidate=@code and exam+type='GO' and len(rtrim(area))>0 and isnull(status,'') in ('T','R')
return @result
end
