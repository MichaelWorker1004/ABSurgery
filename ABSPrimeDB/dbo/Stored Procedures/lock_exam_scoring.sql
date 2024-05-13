CREATE PROCEDURE [dbo].[lock_exam_scoring]
    @ExamscheduleId int
AS
UPDATE
    exam_schedules
SET
    IsLocked = 1
WHERE
    Id = @ExamscheduleId