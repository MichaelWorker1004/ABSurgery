CREATE PROCEDURE [dbo].[ins_exam_schedule_scores]
    @ExamScheduleId INT,
    @ExaminerUserId INT
AS

IF EXISTS(SELECT Id FROM exam_schedule_scores WHERE ExamScheduleId=@ExamScheduleId AND ExaminerUserId=@ExaminerUserId)
BEGIN
    UPDATE exam_schedule_scores
    SET
        ExaminerScore = (SELECT SUM(exam_scoring.score) FROM exam_scoring WHERE exam_scoring.ExamCaseId IN (SELECT exam_cases.Id FROM exam_cases WHERE exam_cases.ExamScheduleId=@ExamScheduleId ) AND exam_scoring.ExaminerUserId=@ExaminerUserId),
        Submitted = 1,
        SubmittedDateTimeUTC = GETUTCDATE(),
        LastUpdatedByUserId = @ExaminerUserId,
        LastUpdatedAtUtc = GETUTCDATE()
    WHERE
        ExamScheduleId=@ExamScheduleId AND 
        ExaminerUserId=@ExaminerUserId
END
ELSE
BEGIN
    INSERT INTO exam_schedule_scores
    (
        ExamScheduleId,
        ExaminerUserId,
        ExaminerScore,
        Submitted,
        SubmittedDateTimeUTC,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    VALUES
    (
        @ExamScheduleId,
        @ExaminerUserId,
        (SELECT SUM(exam_scoring.score) FROM exam_scoring WHERE exam_scoring.ExamCaseId IN (SELECT exam_cases.Id FROM exam_cases WHERE exam_cases.ExamScheduleId=@ExamScheduleId ) AND exam_scoring.ExaminerUserId=@ExaminerUserId),
        1,
        GetUtcDate(),
        @ExaminerUserId,
        @ExaminerUserId
    )
END

-- find next exam_schedule entry (by row number), then set it to active session if current exam_schedule entry is a current session for this examiner
    DECLARE @NextRowNumber INT
    -- SELECT @NextRowNumber = SessionNumber + 1 FROM exam_schedules WHERE id=@ExamScheduleId
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

    IF (@IsAssociateExaminer=1 and @AssociateIsCurrentSession=1)
    BEGIN
        UPDATE exam_schedules SET AssociateIsCurrentSession=0 WHERE id=@ExamScheduleId
    END
    ELSE IF @ExaminerIsCurrentSession=1
    BEGIN
        UPDATE exam_schedules SET ExaminerIsCurrentSession=0 WHERE id=@ExamScheduleId
    END

    IF (@IsAssociateExaminer=1 and @AssociateIsCurrentSession=1)
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

DECLARE @Id INT
SET @Id = @@IDENTITY
exec [dbo].[get_exam_schedule_scores] @Id, @ExaminerUserId