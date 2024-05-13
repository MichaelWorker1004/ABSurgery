CREATE FUNCTION udf_get_fee_received_date
                                           (
                                                   @inv_num VARCHAR(16)
                                           )
RETURNS smalldatetime AS
BEGIN
        DECLARE @result smalldatetime,
                @balance MONEY
        BEGIN
                SET @balance =dbo.udf_get_balance(@inv_num);
                SET @result  =NULL;
                IF @balance <=0
                BEGIN
                        SELECT @result=MAX(trans_time)
                        FROM   fee_received
                        WHERE  amount              > 0
                        AND    fee_received.inv_num=@inv_num;
                
                END
                RETURN @result
        END
END