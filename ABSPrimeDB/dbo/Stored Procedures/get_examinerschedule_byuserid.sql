CREATE PROCEDURE [dbo].[get_examinerschedule_byuserid]
    @ExaminerUserId int,
    @ExamDate date
AS
SELECT
    T.FirstName,
    T.MiddleName,
    T.LastName,
    T.SessionNumber,
    T.StartTime,
    T.EndTime
FROM (
SELECT
    up.FirstName,
    up.MiddleName,
    up.LastName,
    es.SessionNumber,
    es.StartTime                                       AS SortTime,
    es.StartTime,
    es.EndTime
    --FORMAT(CAST(es.StartTime AS DATETIME), 'hh:mm tt') AS StartTime,
    --FORMAT(CAST(es.EndTime AS DATETIME), 'hh:mm tt')   AS EndTime
FROM exam_schedules es
JOIN user_profiles up ON up.UserId = es.ExamineeUserId
WHERE es.ExamDate = @ExamDate
    AND (es.ExaminerUserId = @ExaminerUserId OR es.AssociateExaminerUserId = @ExaminerUserId)
) AS T
ORDER BY T.SortTime