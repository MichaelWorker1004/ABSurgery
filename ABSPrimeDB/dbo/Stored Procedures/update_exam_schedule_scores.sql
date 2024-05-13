CREATE PROCEDURE [dbo].[update_exam_schedule_scores]
    @ExamScheduleScoreId INT,
    @ExaminerScore INT,
    @ExaminerUserId INT
AS
    UPDATE exam_schedule_scores
    SET
        ExaminerScore = @ExaminerScore,
        LastUpdatedByUserId = @ExaminerUserId,
        LastUpdatedAtUtc = GetUtcDate()
    WHERE
        exam_schedule_scores.Id = @ExamScheduleScoreId
        and exam_schedule_scores.ExaminerUserId = @ExaminerUserId

exec [dbo].[get_exam_schedule_scores] @ExamScheduleScoreId, @ExaminerUserId