CREATE PROCEDURE [dbo].[get_examiner_agenda]
    @ExaminerUserId INT,
    @ExamHeaderId INT
AS
SELECT
    id,
    DocumentName
FROM
    user_documents
WHERE
    DocumentName LIKE '%-examineragenda-'+(SELECT examcode from Exam_Directory where id= @ExamHeaderId)+'%' AND UserId=@ExaminerUserId