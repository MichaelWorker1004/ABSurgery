CREATE PROCEDURE [dbo].[get_examination_scores]
    @ExaminerUserId int,
    @ExamHeaderId int
AS
    SELECT
        exam_schedules.Id AS ExamScheduleId,
        exam_schedules.DayNumber,
        exam_schedules.SessionNumber,
        exam_schedules.Roster,
        user_profiles.FirstName+ ' ' + user_profiles.LastName AS DisplayName,
        exam_schedule_scores.Submitted isSubmitted,
        exam_schedules.ExamDate
    FROM
        exam_schedules
    LEFT JOIN
        user_profiles ON user_profiles.UserId=exam_schedules.ExamineeUserId
    INNER JOIN
        exam_schedule_links esl on exam_schedules.Id = esl.ExamScheduleId and esl.ExamHeaderId = @ExamHeaderId
    LEFT JOIN
        exam_schedule_scores on exam_schedule_scores.ExamScheduleId = exam_schedules.Id AND exam_schedule_scores.ExaminerUserId=@ExaminerUserId
    WHERE
        exam_schedules.ExaminerUserId=@ExaminerUserId or exam_schedules.AssociateExaminerUserId=@ExaminerUserId
    ORDER BY DayNumber, SessionNumber, RowNumber ASC