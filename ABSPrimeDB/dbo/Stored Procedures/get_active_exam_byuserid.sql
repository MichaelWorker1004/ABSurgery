CREATE PROCEDURE [dbo].[get_active_exam_byuserid]
	@ExaminerUserId INT,
    @CurrentDate Date = NULL
AS
    IF (@CurrentDate IS NULL or @CurrentDate='')
    BEGIN
        SET @CurrentDate = CONVERT(date, GetUtcDate() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time')
        
    END

    SELECT TOP 1
        ed.Id ExamHeaderId
    FROM Exam_Directory ed
        INNER JOIN exam_schedule_links esl ON esl.ExamHeaderId=ed.Id
        INNER JOIN exam_schedules es ON esl.ExamScheduleId=es.Id AND (es.ExaminerUserId=@ExaminerUserId OR es.AssociateExaminerUserId=@ExaminerUserId)
    WHERE 
        ed.ExaminerOpenDate<=@CurrentDate AND ed.ExamEndDate>=@CurrentDate
