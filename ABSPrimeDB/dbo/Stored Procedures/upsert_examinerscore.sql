CREATE PROCEDURE [dbo].[upsert_examinerscore]
    @ExaminerUserId int,
    @ExamineeUserId int,
    @ExamCaseId int,
    @Score int,
    @Remarks varchar(2000),
    @CriticalFail bit
AS

SET NOCOUNT ON
DECLARE @Id INT
IF (SELECT isLocked from exam_schedules where Id=(SELECT ExamScheduleId FROM exam_cases WHERE Id = @ExamCaseId))= 0
BEGIN
SELECT 
    @Id = Id
FROM 
    exam_scoring 
WHERE 
    ExaminerUserId = @ExaminerUserId 
    AND ExamineeUserId = @ExamineeUserId 
    AND ExamCaseId = @ExamCaseId

IF IsNull(@Id, 0) > 0
BEGIN
    UPDATE 
        exam_scoring
    SET
        Score=@Score,
        Remarks=@Remarks,
        CriticalFail=@CriticalFail,
        LastUpdatedByUserId=@ExaminerUserId,
        LastUpdatedAtUtc=GetUtcDate()
    WHERE
        exam_scoring.Id = @Id

    EXEC [dbo].[get_examcasescore_byid] @Id, @ExaminerUserId
END
ELSE
BEGIN
    INSERT INTO 
        exam_scoring
        (
            ExaminerUserId,
            ExamineeUserId,
            ExamCaseId,
            Score,
            Remarks,
            CriticalFail,
            CreatedByUserId,
            LastUpdatedByUserId
        )
        VALUES
        (
            @ExaminerUserId,
            @ExamineeUserId,
            @ExamCaseId,
            @Score,
            @Remarks,
            @CriticalFail,
            @ExaminerUserId,
            @ExaminerUserId
        )

    DECLARE @NewId INT
    SET @NewId = @@IDENTITY
    EXEC [dbo].[get_examcasescore_byid] @NewId, @ExaminerUserId

END
END
