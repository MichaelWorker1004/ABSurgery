CREATE PROCEDURE [dbo].[get_cases_scored]
    @ExamScheduleId INT,
    @ExaminerUserId INT
AS
    SELECT IIF(COUNT(exam_scoring.Id) = 4, 1, 0) AS CasesScored
    FROM exam_scoring
    WHERE exam_scoring.ExamCaseId IN (SELECT exam_cases.Id FROM exam_cases WHERE exam_cases.ExamScheduleId=@ExamScheduleId ) AND exam_scoring.ExaminerUserId=@ExaminerUserId