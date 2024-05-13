
CREATE FUNCTION udf_backlog_app ( @exam char(1),@type char(1),@year smallint,@weeknum tinyint)
RETURNS smallint
AS
BEGIN
declare @backlog smallint
select @backlog=count(*)
from track 
where year=@year and type=@type and exam = @exam and 
DATEPART ( ww , app_receive )<= @weeknum and  (DATEPART ( ww , app_approve )>@weeknum or (case year(isnull(app_approve,'')) when 1900 then '' else isnull(convert(varchar(10),app_approve,101),'') end)='')
return @backlog
end


