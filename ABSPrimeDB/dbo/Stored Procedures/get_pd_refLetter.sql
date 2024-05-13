CREATE PROCEDURE [dbo].[get_pd_refLetters]
    @UserId INT,
    @ExamId INT
AS
BEGIN
    SET NOCOUNT ON;
DECLARE @ExamCode VARCHAR(8)
SELECT @ExamCode = ExamCode FROM dbo.Exam_Directory WHERE Id = @ExamId
SELECT
    AppRefLet.id,
    AppRefLet.UserId,
    Hosp,
    Official,
    Title,
    Email,
    Let_sent as LetterSent,
    IIF(let_approve IS NULL AND let_rcvd IS NULL,1,IIF(let_approve IS NULL AND let_rcvd IS NOT NULL,2,3 )) Status

FROM
    dbo.AppRefLet
WHERE
    userId = @UserId AND exam_type = @ExamCode
END