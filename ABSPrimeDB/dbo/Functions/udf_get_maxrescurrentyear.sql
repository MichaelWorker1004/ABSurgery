CREATE FUNCTION udf_get_maxrescurrentyear ( @prognum varchar(10),@exam varchar(5))
RETURNS varchar(20)
AS
BEGIN
declare @mdate as varchar(20)
select @mdate=(select max(current_year) from resident where program=@prognum and exam=@exam)
return isnull(@mdate,'')
end




