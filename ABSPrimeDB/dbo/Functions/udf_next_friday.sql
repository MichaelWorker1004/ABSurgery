CREATE FUNCTION udf_next_friday ( @year smallint,@weeknum tinyint)
RETURNS varchar(11)
AS
BEGIN
declare @result varchar(11)
select @result=convert(varchar(11),dateadd(d,case datename(dw,DATEADD (wk,@weeknum,'01/01/'+cast(@year as varchar(4)))) 
  when 'Monday' then 4 
  when 'Tuesday' then 3 
  when 'Wednesday' then 2 
  when 'Thursday' then 1 
  when 'Friday' then 0 
  when 'Saturday' then 5 
  when 'Sunday' then 6 end,DATEADD (wk,@weeknum-1,'01/01/'+cast(@year as varchar(4)))),106)

return @result
end





