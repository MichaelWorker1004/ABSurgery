--drop FUNCTION udf_get_prsninfo_lastupdt
CREATE FUNCTION udf_get_prsninfo_lastupdt(@candidate char(6))
RETURNS smalldatetime
AS
begin
	DECLARE @result smalldatetime,@basedate smalldatetime
	select @result=max(last_updt) from app_response where candidate = @candidate and charindex('B',exam_type)>0
	if not @result is null
	begin
		select @basedate = cast(attr as smalldatetime) from mcodes where code='PI' and grp='MS'
		if @result<@basedate 
		begin
			set @result = null
		end
		
	end
	return @result
end


