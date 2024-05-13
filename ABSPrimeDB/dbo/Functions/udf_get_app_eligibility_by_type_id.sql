CREATE FUNCTION udf_get_app_eligibility_by_type_id(@candidate varchar(6),@app_type as varchar(8),@app_id as numeric)
RETURNS bit
AS
BEGIN
	declare @result bit
	set @result=0
	
		if exists(select candidate from surgeon_st where status_code='AO' and candidate=@candidate and CHARINDEX(@app_type, st_val)>0 )
	begin	
		set @result=1
	end
	else if exists(select candidate from dscpl_action where effective=1 and candidate=@candidate and dscpl_code in ('CR','CS'))
	begin	
		set @result=0
	end
	else if (@app_id=15)	begin	
	if exists (select candidate from exam where 
		candidate=@candidate and 
		/*cast(year as varchar)+exam+type+rtrim(area)+'E'=@app_type and 
		rply_sent is not null and 
		year(rply_sent)!=1900 and 
		*/
		cast(year as varchar)+exam+type+'E'=@app_type and 
		rply_rcvd is null)
		begin
			set @result=1
		end
	end

	return @result
end