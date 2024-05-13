CREATE PROCEDURE [dbo].[get_isLocked_by_examId]
    @ExamCaseId int
AS
    SELECT
        IsLocked
    FROM
        exam_schedules
    WHERE
        Id = (SELECT ExamScheduleId FROM exam_cases WHERE Id = @ExamCaseId)