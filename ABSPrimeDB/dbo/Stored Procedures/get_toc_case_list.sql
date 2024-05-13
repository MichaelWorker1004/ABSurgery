CREATE PROCEDURE [dbo].[get_toc_case_list]
    @ScheduleId1 int,
    @ScheduleId2 int = NULL
AS
    DECLARE @ExamDate datetime, @DayNumber INT, @SessionNumber INT, @SessionNumber2 INT
    SELECT @ExamDate=ExamDate, @DayNumber=DayNumber, @SessionNumber=SessionNumber FROM exam_schedules WHERE Id=@ScheduleId1
    SELECT  @SessionNumber2=SessionNumber FROM exam_schedules WHERE Id=ISNULL(@ScheduleId2,0)
    SELECT DISTINCT
        case_headers.CaseNumber,
        case_headers.Description,
        case_headers.Title,
        case_headers.Id,
        exam_schedules.RowNumber,
        exam_cases.SortOrder
    FROM
        case_headers
    LEFT JOIN
            exam_cases on case_headers.Id = exam_cases.CaseId
    LEFT JOIN
            exam_schedule_links esl ON esl.ExamScheduleId=@ScheduleId1
    LEFT JOIN 
            Exam_Directory ed ON ed.Id=esl.ExamHeaderId
    LEFT JOIN
            exam_specialties es ON es.Id=ed.ExamSpecialtyId
    -- WHERE exam_cases.ExamScheduleId IN (@ScheduleId1, @ScheduleId2)
    -- temporary workaround for exam case rosters to display all cases
    LEFT JOIN exam_schedules ON exam_schedules.Id=exam_cases.ExamScheduleId
    WHERE exam_schedules.ExamDate=@ExamDate AND exam_schedules.DayNumber=@DayNumber 
        AND (exam_schedules.SessionNumber=@SessionNumber or (es.Code IN ('O', 'P') and exam_schedules.SessionNumber=@SessionNumber2)) -- only grab cases from both sessions if CGSO or Peds exam 
    ORDER BY exam_schedules.RowNumber,exam_cases.SortOrder
RETURN 0