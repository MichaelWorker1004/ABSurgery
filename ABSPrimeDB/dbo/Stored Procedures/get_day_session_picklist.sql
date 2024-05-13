CREATE PROCEDURE [dbo].[get_day_session_picklist]
    @ExaminerUserId INT,
    @CurrentDate DATETIME
AS

DECLARE @ExamHeaderId INT, @ExamCode VARCHAR(10), @SpecialtyCode VARCHAR(1)

SELECT @ExamHeaderId=ed.Id, @ExamCode=ExamCode, @SpecialtyCode=es.Code
FROM Exam_Directory ed
LEFT JOIN exam_specialties es ON es.Id=ed.ExamSpecialtyId
WHERE @CurrentDate BETWEEN ExaminerOpenDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC' AND ExamEndDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC'
        AND ed.Id IN (SELECT DISTINCT ExamHeaderId FROM exam_schedules es INNER JOIN exam_schedule_links esl ON es.Id = esl.ExamScheduleId WHERE es.ExaminerUserId=@ExaminerUserId OR es.AssociateExaminerUserId=@ExaminerUserId)

IF @SpecialtyCode NOT IN ('O', 'P')
BEGIN
    SELECT DISTINCT
        'Exam ' + @ExamCode + ' Day ' + CAST(exam_schedules.DayNumber AS VARCHAR) +' Session '+ CAST(exam_schedules.SessionNumber AS VARCHAR) + IIF(ES2.SessionNumber IS NULL,'',' & '+CAST(ES2.SessionNumber AS VARCHAR)) AS ExamSchedule,
        -- exam_schedules.Id AS Session1Id,
        -- ES2.Id AS Session2Id,
        MIN(exam_schedules.Id) AS Session1Id,
        MIN(ES2.Id) AS Session2Id
    FROM
        exam_schedules
    INNER JOIN
        exam_schedule_links ON exam_schedules.Id = exam_schedule_links.ExamScheduleId
    LEFT JOIN
        exam_schedules ES2 ON exam_schedules.DayNumber = ES2.DayNumber AND exam_schedules.ExamDate = ES2.ExamDate AND exam_schedules.Roster = ES2.Roster AND exam_schedules.SessionNumber+1 = ES2.SessionNumber --and (ES2.ExaminerUserId=@ExaminerUserId OR ES2.AssociateExaminerUserId=@ExaminerUserId)
    -- INNER JOIN 
    --     Exam_Directory ed ON @CurrentDate BETWEEN ExaminerOpenDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC' AND ExamEndDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC'
    --         AND ed.Id IN (SELECT DISTINCT ExamHeaderId FROM exam_schedules es INNER JOIN exam_schedule_links esl ON es.Id = esl.ExamScheduleId WHERE es.ExaminerUserId=@ExaminerUserId OR es.AssociateExaminerUserId=@ExaminerUserId)
    WHERE
        -- (exam_schedules.ExaminerUserId=@ExaminerUserId
        -- OR exam_schedules.AssociateExaminerUserId=@ExaminerUserId) AND
        exam_schedule_links.ExamHeaderId=@ExamHeaderId
        AND IIF(ES2.SessionNumber IS NULL,1,IIF(exam_schedules.SessionNumber!>ES2.SessionNumber,1,0))=1 --first session number always bigger (unless no next session)
        AND exam_schedules.SessionNumber%2=1 --first session number always odd
    GROUP BY exam_schedules.DayNumber, exam_schedules.SessionNumber, ES2.SessionNumber
END
ELSE
BEGIN
    SELECT DISTINCT
        'Exam ' + @ExamCode + ' Day ' + CAST(exam_schedules.DayNumber AS VARCHAR) +' Session '+ CAST(exam_schedules.SessionNumber AS VARCHAR) AS ExamSchedule,
        MIN(exam_schedules.Id) AS Session1Id,
        NULL AS Session2Id
    FROM
        exam_schedules
    INNER JOIN
        exam_schedule_links ON exam_schedules.Id = exam_schedule_links.ExamScheduleId
    -- INNER JOIN
    --     Exam_Directory ed ON @CurrentDate BETWEEN ExaminerOpenDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC' AND ExamEndDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC'
    --         AND ed.Id IN (SELECT DISTINCT ExamHeaderId FROM exam_schedules es INNER JOIN exam_schedule_links esl ON es.Id = esl.ExamScheduleId WHERE es.ExaminerUserId=@ExaminerUserId OR es.AssociateExaminerUserId=@ExaminerUserId)
    WHERE
        exam_schedule_links.ExamHeaderId=@ExamHeaderId
    GROUP BY exam_schedules.DayNumber, exam_schedules.SessionNumber
END
RETURN 0