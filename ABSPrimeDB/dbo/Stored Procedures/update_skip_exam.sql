CREATE PROCEDURE [dbo].[update_skip_exam]
    @ExamScheduleId INT,
    @ExaminerUserId INT
AS
-- find next exam_schedule entry (by session number), then set it to active session
    DECLARE @NextRowNumber INT
    -- SELECT @NextRowNumber = RowNumber + 1 FROM exam_schedules WHERE id=@ExamScheduleId
    DECLARE @ExamDate datetime
    DECLARE @ExaminerIsCurrentSession INT, @AssociateIsCurrentSession INT
    SELECT 
        @NextRowNumber = RowNumber + 1 
        ,@ExamDate = ExamDate
        ,@ExaminerIsCurrentSession = ExaminerIsCurrentSession
        ,@AssociateIsCurrentSession = AssociateIsCurrentSession 
    FROM exam_schedules WHERE id=@ExamScheduleId
    
    DECLARE @IsAssociateExaminer BIT
    SELECT @IsAssociateExaminer = (SELECT IIF((EXISTS(SELECT * from exam_schedules where AssociateExaminerUserId = @ExaminerUserId AND Id=@ExamScheduleId)),1,0))

    IF(@IsAssociateExaminer=1 AND @AssociateIsCurrentSession=1)
    BEGIN
        UPDATE exam_schedules SET AssociateIsCurrentSession=0 WHERE id=@ExamScheduleId
    END
    ELSE IF @ExaminerIsCurrentSession=1
    BEGIN
        UPDATE exam_schedules SET ExaminerIsCurrentSession=0 WHERE id=@ExamScheduleId
    END

    IF (@IsAssociateExaminer=1 AND @AssociateIsCurrentSession=1)
    BEGIN
        UPDATE exam_schedules SET AssociateIsCurrentSession=1 
        WHERE ExamDate=@ExamDate AND AssociateExaminerUserId=@ExaminerUserId
            AND RowNumber=(
                SELECT MIN(RowNumber) session FROM exam_schedules es
                LEFT JOIN exam_schedule_scores ess ON ess.ExamScheduleId=es.Id AND ess.ExaminerUserId=@ExaminerUserId AND Submitted='1'
                WHERE es.AssociateExaminerUserId=@ExaminerUserId and ExamDate=@ExamDate and RowNumber>=@NextRowNumber and ess.Id IS NULL
            )
    END
    ELSE IF @ExaminerIsCurrentSession=1
    BEGIN
        UPDATE exam_schedules SET ExaminerIsCurrentSession=1 
        WHERE ExamDate=@ExamDate AND ExaminerUserId=@ExaminerUserId
            AND RowNumber=(
                SELECT MIN(RowNumber) session FROM exam_schedules es
                LEFT JOIN exam_schedule_scores ess ON ess.ExamScheduleId=es.Id AND ess.ExaminerUserId=@ExaminerUserId AND Submitted='1'
                WHERE es.ExaminerUserId=@ExaminerUserId and ExamDate=@ExamDate and RowNumber>=@NextRowNumber and ess.Id IS NULL
            )
    END
