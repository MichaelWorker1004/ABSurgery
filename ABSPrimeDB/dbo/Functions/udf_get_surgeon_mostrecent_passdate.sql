CREATE FUNCTION udf_get_surgeon_mostrecent_passdate ( @candidate char(6))
RETURNS varchar(50)
AS
BEGIN
declare @result varchar(50)
select @result = (SELECT max([date]) 
FROM	exam
WHERE	exam.[candidate]=@candidate and exam.[result]='P')
return isnull(@result,'')
end


