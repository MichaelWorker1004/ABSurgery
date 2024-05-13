CREATE FUNCTION udf_get_inv_tot (@inv_num varchar(16))
RETURNS money
AS
BEGIN
DECLARE @result money,@examtype varchar(3)
	begin
		if (right(@inv_num,1)!='A' and right(@inv_num,1)!='E' and left(@inv_num,1)!='P') 		begin
			set @examtype=right(@inv_num,2)
			set @inv_num=left(@inv_num,11)+dbo.udf_type_te(left(@examtype,1),right(@examtype,1))+'A'
		end		
		
		select @result=isnull(owed,0)
			from invoice 
			left join             
				(select inv_num,sum(amount*quantity) owed from inv_det group by inv_num) c on invoice.inv_num=c.inv_num 
			where invoice.inv_num=@inv_num
	end

RETURN @result
END