CREATE PROCEDURE [dbo].[get_current_qualifying_exam]
AS
SELECT TOP 1
    CAST(DATEPART(YEAR,GETUTCDATE())AS VARCHAR) + ' ' + exam_specialties.Name + ' ' + exam_types.Name AS ExamName,
    Exam_Directory.RegOpenDate,
    Exam_Directory.RegEndDate,
    Exam_Directory.ExamStartDate,
    Exam_Directory.ExamEndDate

FROM
    Exam_Directory
LEFT JOIN
    exam_specialties ON exam_specialties.Id= Exam_Directory.ExamSpecialtyId
LEFT JOIN
    exam_types ON exam_types.Id= Exam_Directory.ExamTypeId
WHERE
    Exam_Directory.ExamSpecialtyId=1 AND
    exam_types.CODE='Q' AND
    Exam_Directory.RegEndDate>GETUTCDATE()
ORDER BY RegOpenDate