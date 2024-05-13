CREATE PROCEDURE [dbo].[get_registration_open]
    @examCode VARCHAR(50)
AS
SELECT
    RegOpenDate,
    RegEndDate,
    IIF(RegOpenDate<=GETUTCDATE() AND GETUTCDATE()>RegEndDate,1,0) isRegOpen,
    RegLateDate,
    IIF(RegLateDate<=GETUTCDATE() AND GETUTCDATE()>RegEndDate,1,0) isRegLate
FROM
    Exam_Directory
WHERE
    ExamCode=@examCode