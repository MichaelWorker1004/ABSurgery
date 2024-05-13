CREATE PROCEDURE [dbo].[get_examinee_sessions]
    @ExaminerUserId int,
    @ExamDate datetime
AS
SELECT
    T.ExamScheduleId,
    T.FirstName,
    T.LastName,
    T.StartTime,
    T.EndTime,
    T.MeetingLink,
    T.IsSubmitted,
    T.IsCurrentSession,
    T.SessionNumber,
    T.isLocked
 FROM (
SELECT DISTINCT
    exam_schedules.Id AS ExamScheduleId,
    user_profiles.FirstName,
    user_profiles.LastName,
    exam_schedules.StartTime AS SortTime,
    FORMAT(CONVERT(datetime, exam_schedules.StartTime), 'hh:mm tt') AS StartTime,
    FORMAT(CONVERT(datetime, exam_schedules.EndTime), 'hh:mm tt') AS EndTime,
    exam_schedules.MeetingLink,
    ISNULL(exam_schedule_scores.Submitted, 0) AS IsSubmitted,
    IIF(exam_schedules.ExaminerUserId= @ExaminerUserId, exam_schedules.ExaminerIsCurrentSession, exam_schedules.AssociateIsCurrentSession) AS IsCurrentSession,
    exam_schedules.SessionNumber,
    exam_schedules.isLocked
FROM
    exam_schedules
LEFT JOIN
        user_profiles ON exam_schedules.ExamineeUserId = user_profiles.UserId
LEFT JOIN
        exam_cases  on exam_schedules.Id = exam_cases.ExamScheduleId
LEFT JOIN
        exam_scoring on exam_cases.Id = exam_scoring.ExamCaseId
LEFT JOIN
        exam_schedule_scores on exam_schedule_scores.ExaminerUserId = @ExaminerUserId AND exam_schedule_scores.ExamScheduleId = exam_schedules.Id
WHERE
    (exam_schedules.ExaminerUserId = @ExaminerUserId
     OR exam_schedules.AssociateExaminerUserId = @ExaminerUserId)
    AND exam_schedules.ExamDate = @ExamDate
) AS T
ORDER BY
    T.SortTime
RETURN 0