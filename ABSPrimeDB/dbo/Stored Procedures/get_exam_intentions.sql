CREATE PROCEDURE [dbo].[get_exam_intentions]
    @UserId INT,
    @ExamId INT
AS
    SELECT
        Exam_Directory.ExamName ExamName,
        IIF( Exam_Directory.ExamFormatId=2, DATEADD(DAY, ISNULL(exam.day, 1)-1, Exam_Directory.ExamStartDate), NULL) ExamDate,
        exam.rply_rcvd DateReceived,
        exam.istaking Intention
    FROM
        exam
    LEFT JOIN Exam_Directory ON Exam_Directory.Id = exam.examHeaderId
    LEFT JOIN exam_specialties ON exam.examSpecialtyId = exam_specialties.id
    LEFT JOIN exam_types ON exam.examTypeId = exam_types.id
    WHERE
        exam.UserId = @UserId
        AND exam.examHeaderId = @ExamId