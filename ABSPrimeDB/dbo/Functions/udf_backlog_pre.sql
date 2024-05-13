CREATE FUNCTION udf_backlog_pre ( @exam char(1),@type char(1),@year smallint,@weeknum tinyint)
RETURNS smallint
AS
BEGIN
declare @backlog smallint
select @backlog=count(*)
from track 
where year=@year and type=@type and exam = @exam and 
DATEPART ( ww , pre_receive )<= @weeknum and  (DATEPART ( ww , pre_approve )>@weeknum or (case year(isnull(pre_approve,'')) when 1900 then '' else isnull(convert(varchar(10),pre_approve,101),'') end)='')
return @backlog
end


