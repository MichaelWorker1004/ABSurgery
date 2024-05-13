CREATE PROCEDURE [dbo].[Get_Exam_Fees_byUserId]
    @UserId INT
AS
SELECT Balance.*,
       AmountPaid.PaidTotal,
       Balance.SubTotal-ISNULL(AmountPaid.PaidTotal,0) BalanceDue,
       IIF(invoice.type='A',CAST(track.year AS VARCHAR)+track.exam+track.type ,CAST(exam.year AS VARCHAR)+exam.exam+exam.type ) ExamCode,
        fee_received.trans_time PaymentDate
FROM(
    SELECT
        SUM(inv_det.amount*inv_det.quantity) SubTotal,
        inv_det.inv_num InvoiceNumber
    FROM inv_det
    WHERE inv_det.UserId=@UserId GROUP BY inv_det.inv_num) Balance
LEFT JOIN dbo.invoice on invoice.inv_num=Balance.InvoiceNumber
LEFT JOIN exam on exam.id=invoice.ref_id AND exam.UserId=@UserId
LEFT JOIN track on track.id=invoice.ref_id AND track.UserId=@UserId
LEFT JOIN (
    SELECT
        SUM(fee_received.amount) PaidTotal,
        fee_received.inv_num InvoiceNumber
    FROM fee_received
    WHERE fee_received.UserId=@UserId GROUP BY fee_received.inv_num)AmountPaid ON AmountPaid.InvoiceNumber=Balance.InvoiceNumber
LEFT JOIN fee_received on fee_received.inv_num=AmountPaid.InvoiceNumber and fee_received.id = (SELECT MAX(id) FROM fee_received WHERE inv_num=AmountPaid.InvoiceNumber)
ORDER BY invoice.created DESC
