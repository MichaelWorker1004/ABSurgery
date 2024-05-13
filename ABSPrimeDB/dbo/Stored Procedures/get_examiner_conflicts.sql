CREATE PROCEDURE [dbo].[get_examiner_conflicts]
    @ExaminerUserId INT,
    @ExamHeaderId INT
AS
SELECT
    id,
    DocumentName
FROM
    user_documents
WHERE
    DocumentName LIKE '%'+CAST(@ExaminerUserId AS VARCHAR)+'-examinerconflicts-'+(SELECT examcode from Exam_Directory where id= @ExamHeaderId)+'%' AND UserId=@ExaminerUserId