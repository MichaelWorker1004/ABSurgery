CREATE PROCEDURE [dbo].[dev_reset_scoring_by_ExaminerId]
    @ExaminerUserId int
AS

DECLARE @IsAssociateExaminer BIT
SELECT @IsAssociateExaminer =(SELECT IIF((EXISTS(SELECT * from exam_schedules where AssociateExaminerUserId = @ExaminerUserId)),1,0))

DELETE FROM exam_schedule_scores WHERE ExaminerUserId=@ExaminerUserId;
DELETE FROM exam_scoring WHERE ExaminerUserId=@ExaminerUserId;

IF (@IsAssociateExaminer=1)
BEGIN
    UPDATE exam_schedules SET AssociateIsCurrentSession=IIF(RowNumber=1, 1, 0) WHERE AssociateExaminerUserId=@ExaminerUserId
END
ELSE
BEGIN
    UPDATE exam_schedules SET ExaminerIsCurrentSession=IIF(RowNumber=1, 1, 0) WHERE ExaminerUserId=@ExaminerUserId
END
go

