CREATE PROCEDURE [dbo].[get_examinee_session_byid]
	@ExamScheduleId INT
AS
SELECT
    exam_schedules.Id as ExamScheduleId,
    user_profiles.FirstName + ' ' + user_profiles.LastName AS FullName,
    exam_schedules.ExamineeUserId,
    exam_schedule_scores.Id as ExamScoringId,
    CAST((exam_schedules.ExamDate+CAST(exam_schedules.StartTime AS DATETIME)) AS VARCHAR) ExamDate,
    Exam_Directory.TimerBit
FROM
    dbo.exam_schedules
LEFT JOIN
    dbo.user_profiles ON user_profiles.UserID = exam_schedules.ExamineeUserId
LEFT JOIN
    exam_schedule_scores ON exam_schedule_scores.ExamScheduleId = exam_schedules.Id
LEFT JOIN
    exam_schedule_links ON exam_schedule_links.ExamScheduleId = exam_schedules.Id
LEFT JOIN
    Exam_Directory ON Exam_Directory.Id = exam_schedule_links.ExamHeaderId
WHERE
    exam_schedules.Id=@ExamScheduleId