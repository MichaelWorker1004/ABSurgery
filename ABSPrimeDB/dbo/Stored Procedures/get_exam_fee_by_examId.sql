CREATE PROCEDURE [dbo].[get_exam_fee_by_examId]
    @UserId INT,
    @ExamId INT
AS
SELECT Balance.*,
       AmountPaid.PaidTotal,
       Balance.SubTotal-ISNULL(AmountPaid.PaidTotal,0) BalanceDue,
       CAST(exam.year AS VARCHAR)+exam.exam+exam.type ExamCode,
       fee_received.trans_time PaymentDate
FROM(
    SELECT
        SUM(inv_det.amount*inv_det.quantity) SubTotal,
        inv_det.inv_num InvoiceNumber
    FROM inv_det
    WHERE inv_det.UserId=@UserId GROUP BY inv_det.inv_num) Balance
INNER JOIN dbo.invoice on invoice.inv_num=Balance.InvoiceNumber and invoice.type='E'
LEFT JOIN Exam_Directory on Exam_Directory.Id=@ExamId
INNER JOIN exam on exam.id=invoice.ref_id and exam.examHeaderId=Exam_Directory.Id and exam.UserId=@UserId
LEFT JOIN (
    SELECT
        SUM(fee_received.amount) PaidTotal,
        fee_received.inv_num InvoiceNumber
    FROM fee_received
    WHERE fee_received.UserId=@UserId GROUP BY fee_received.inv_num)AmountPaid ON AmountPaid.InvoiceNumber=Balance.InvoiceNumber
LEFT JOIN fee_received on fee_received.inv_num=AmountPaid.InvoiceNumber and fee_received.id = (SELECT MAX(id) FROM fee_received WHERE inv_num=AmountPaid.InvoiceNumber)