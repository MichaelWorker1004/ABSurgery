CREATE PROCEDURE [dbo].[ins_pd_refLetter]
    @UserId INT,
    @Official VARCHAR(100),
    @Title VARCHAR(100),
    @Email VARCHAR(100),
    @Hosp VARCHAR(1000)='',
    @ExamId INT,
    @IdCode VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Candidate VARCHAR(10)
    SELECT @Candidate = AbsId FROM dbo.User_Profiles WHERE UserId = @UserId
    DECLARE @ExamCode VARCHAR(8)
    SELECT @ExamCode = ExamCode FROM dbo.Exam_Directory WHERE Id = @ExamId
    INSERT INTO dbo.AppRefLet
    (Candidate,UserId, hosp, official, title, let_type, alternaterole, explain, email, phone, city, state,exam_type,sec_order,id_code,let_sent)
    VALUES
    (@Candidate,@UserId, @Hosp, @Official, @Title, '10', '', '', @Email, '', '', '',@ExamCode,10,@IdCode,GETDATE())
EXEC get_pd_refLetters @UserId,@ExamId
END
go
