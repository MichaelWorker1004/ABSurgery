CREATE FUNCTION udf_get_inv_num (@app_type varchar(16))
RETURNS varchar(16)
AS
BEGIN
	begin
		DECLARE @examtype varchar(3)
		
		if (right(@app_type,1)!='A' and right(@app_type,1)!='E' and left(@app_type,1)!='P') 		begin
			set @examtype=right(@app_type,2)
			set @app_type=left(@app_type,11)+dbo.udf_type_te(left(@examtype,1),right(@examtype,1))+'A'
		end

		if right(@app_type,2)='OE'
		begin
			SELECT @app_type=candidate+examcode+'E' FROM exam 
			WHERE candidate+cast(year as varchar(4))+exam+type+'E'=@app_type AND status in ('T','R')
		end
	end
	RETURN @app_type
END