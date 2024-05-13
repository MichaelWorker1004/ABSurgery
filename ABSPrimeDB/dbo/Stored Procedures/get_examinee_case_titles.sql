CREATE PROCEDURE [dbo].[get_examinee_case_titles]
    @ExamScheduleId INT,
    @ExaminerUserId INT,
    @ExamineeUserId INT
AS
SELECT
    'Case ' + case_headers.CaseNumber + IIF(case_headers.Description IS NULL,' ',' '+case_headers.Description+' - ')+ case_headers.Title AS Title,
    case_headers.Id AS CaseHeaderId,
    exam_cases.Id AS ExamCaseId,
    exam_scoring.Score,
    exam_scoring.CriticalFail,
    exam_scoring.Remarks
FROM
        dbo.exam_schedules
    LEFT JOIN
        exam_cases ON exam_schedules.Id = exam_cases.ExamScheduleId
    LEFT JOIN
        case_headers ON case_headers.Id = exam_cases.CaseId
    LEFT JOIN
        exam_scoring ON exam_scoring.ExamCaseId = exam_cases.Id AND exam_scoring.ExaminerUserId = @ExaminerUserId AND exam_scoring.ExamineeUserId = @ExamineeUserId
WHERE
    exam_schedules.Id = @ExamScheduleId
