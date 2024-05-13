CREATE PROCEDURE [dbo].[get_exam_title_byExamId]
@ExamId INT
AS
SELECT
    CAST(Exam_Directory.ExamYear AS VARCHAR) +' ' + exam_specialties.Name + ' ' + exam_types.Name AS ExamName
FROM
    Exam_Directory
LEFT JOIN
    exam_specialties ON exam_specialties.Id= Exam_Directory.ExamSpecialtyId
LEFT JOIN
    exam_types ON exam_types.Id= Exam_Directory.ExamTypeId
WHERE
    Exam_Directory.Id=@ExamId