CREATE PROCEDURE [dbo].[get_exam_overview]
	@UserId INT
AS
SELECT
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
    exam_types ON exam_types.Id=Exam_Directory.ExamTypeId
LEFT JOIN exam ON exam.examHeaderId=Exam_Directory.Id AND exam.UserId=@UserId
LEFT JOIN track ON track.year=Exam_Directory.ExamYear AND track.exam=exam_specialties.Code AND track.type=exam_types.Code and track.UserId=@UserId
WHERE
    Exam_Directory.RegOpenDate<GETUTCDATE()
    AND Exam_Directory.ExamEndDate>GETUTCDATE()
    AND (track.UserId IS NOT NULL or exam.UserId IS NOT NULL)