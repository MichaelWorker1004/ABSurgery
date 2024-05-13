CREATE PROCEDURE [dbo].[ins_payment]
    @sslTransactionId VARCHAR(50),
    @sslInvoiceNumber VARCHAR(50),
    @sslLastName VARCHAR(50),
    @sslFirstName VARCHAR(50),
    @sslAmount DECIMAL(10,2),
    @sslTxnTime DATETIME,
    @sslAllParameters VARCHAR(MAX)
AS
    UPDATE inv_det SET trans_id=@sslTransactionId WHERE inv_num=@sslInvoiceNumber AND ISNULL(RTRIM(trans_id),'')='';
    DECLARE @UserId INT;
    SELECT @UserId=UserId FROM inv_det WHERE inv_num=@sslInvoiceNumber;
    IF NOT EXISTS (SELECT inv_num FROM fee_received WHERE trans_id=@sslTransactionId)
    BEGIN
    INSERT INTO fee_received
        (UserId, name, inv_num, amount, trans_id, trans_time, trans_log)
    values
        (@UserId, @sslLastName+' '+@sslFirstName,@sslInvoiceNumber, @sslAmount, @sslTransactionId, @sslTxnTime,@sslAllParameters);

    SELECT @sslInvoiceNumber inv_num,dbo.udf_get_balance(@sslInvoiceNumber) balance;
    END
    ELSE BEGIN SELECT '' inv_num,0 balance; END