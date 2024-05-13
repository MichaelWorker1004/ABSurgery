UPDATE
    exam
SET
    exam.examHeaderId=  Exam_Directory.Id
FROM
    Exam_Directory
INNER JOIN
    exam ON Exam_Directory.ExamCode=CAST(exam.year AS VARCHAR)+exam.exam+exam.type+CAST(exam.area AS VARCHAR)