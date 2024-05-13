--drop FUNCTION udf_get_lastarea
CREATE FUNCTION udf_get_lastarea(@code varchar(6))
RETURNS varchar(6)
AS
BEGIN
DECLARE @result varchar(6)
select @result=cast(year as varchar(4))+'_'+area from exam where 
candidate=@code and exam+type='GO' and len(rtrim(area))>0 and year=(select max(year) from exam 
	where candidate=@code and exam+type='GO' and len(rtrim(area))>0 )
return @result
end




