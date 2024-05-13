CREATE FUNCTION udf_isrepeater (@candidate char(6),@exam char(1),@type char(1),@year smallint)
RETURNS bit
AS
begin
DECLARE 
@result bit
set @result=0
begin
	if exists(select id from exam where candidate=@candidate and exam=@exam and type=@type and year<>@year and isnull(result,'')<>'')
		begin
			set @result=1
		end
end
return @result
end





