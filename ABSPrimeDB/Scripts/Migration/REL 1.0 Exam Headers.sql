INSERT INTO Exam_Directory (ExamCode,  ExamStartDate, ExamEndDate, CreatedByUserId, LastUpdatedByUserId)
SELECT
    Code,
    Attr,
    IIF(ISDATE(Attr2)=1, Attr2, Attr),
    @UserId,
    @UserId
FROM
    mcodes
WHERE
    grp='EC'