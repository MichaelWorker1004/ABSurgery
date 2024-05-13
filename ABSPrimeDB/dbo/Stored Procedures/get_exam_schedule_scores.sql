CREATE PROCEDURE [dbo].[get_exam_schedule_scores]
    @ExamScheduleScoreId INT,
    @ExaminerUserId INT
AS
SELECT
    exam_schedule_scores.Id as ExamScheduleScoreId,
    exam_schedule_scores.ExamScheduleId,
    exam_schedule_scores.ExaminerUserId,
    exam_schedule_scores.ExaminerScore,
    exam_schedule_scores.Submitted,
    exam_schedule_scores.SubmittedDateTimeUTC,
    exam_schedule_scores.CreatedByUserId,
    exam_schedule_scores.CreatedAtUtc,
    exam_schedule_scores.LastUpdatedByUserId,
    exam_schedule_scores.LastUpdatedAtUtc
FROM
    exam_schedule_scores
WHERE
    exam_schedule_scores.Id = @ExamScheduleScoreId
    and exam_schedule_scores.ExaminerUserId = @ExaminerUserId