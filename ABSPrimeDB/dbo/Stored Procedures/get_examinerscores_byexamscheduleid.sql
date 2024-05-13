CREATE PROCEDURE [dbo].[get_examinerscores_byexamscheduleId]
    @ExaminerUserId int,
    @ExamScheduleId int
AS
    SELECT
        esco.Id ExamScoringId,
        ec.Id ExamCaseId,
        ch.CaseNumber CaseNumber,
        @ExaminerUserId ExaminerUserId,
        es.ExamineeUserId,
        up.FirstName ExamineeFirstName,
        up.MiddleName ExamineeMiddleName,
        up.LastName ExamineeLastName,
        up.Suffix ExamineeSuffix,
        es.ExamDate,
        es.StartTime,
        es.EndTime,
        esco.Score,
        esco.CriticalFail,
        esco.Remarks,
        es.isLocked
        --esco.Submitted
    FROM 
        exam_schedules es
        INNER JOIN exam_cases ec ON ec.ExamScheduleId=es.Id
        LEFT JOIN case_headers ch ON ch.Id=ec.CaseId
        LEFT JOIN exam_scoring esco ON esco.ExamCaseId=ec.Id AND esco.ExaminerUserId=@ExaminerUserId
        INNER JOIN user_profiles up ON up.UserId=es.ExamineeUserId
    WHERE 
        es.Id=@ExamScheduleId