CREATE FUNCTION [dbo].[udf_get_cc_application_fee_status](@UserId INT)
RETURNS INT
AS
BEGIN
        DECLARE @Status INT
        SELECT @Status=ISNULL(
        (SELECT 
            CASE 
                WHEN isnull(owed,0)-isnull(paid,0) > 0 THEN 0 --incomplete
                ELSE 1 --complete
            END Status
	FROM invoice 
        LEFT JOIN            
	        (select inv_num,sum(amount) paid from fee_received group by inv_num) b on invoice.inv_num=b.inv_num 
	LEFT JOIN 
	    (select inv_num,sum(amount*quantity) owed from inv_det group by inv_num) c on invoice.inv_num=c.inv_num 
	WHERE invoice.UserId=@UserId AND invoice.inv_num=CAST(@UserId AS VARCHAR)+dbo.udf_get_academic_year('M')+'MCA'), -1) -- -1 = not yet available/contingent

        RETURN @Status
END