CREATE PROCEDURE [dbo].[ins_cc_refLetter]
    @UserId INT,
    @Official VARCHAR(100),
    @Title VARCHAR(100),
    @RoleId VARCHAR(3),
    @AltRoleId VARCHAR(3) = '',
    @Explain VARCHAR(1000) = '',
    @Email VARCHAR(100),
    @Phone VARCHAR(100),
    @Hosp VARCHAR(1000),
    @City VARCHAR(100),
    @State VARCHAR(10),
    @SecOrder INT,
    @IdCode VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Candidate VARCHAR(10)
    SELECT @Candidate = AbsId FROM dbo.User_Profiles WHERE UserId = @UserId
    DECLARE @ExplainText VARCHAR(200)
    SELECT @ExplainText = [Explain] FROM dbo.refLet_explain_picklist WHERE CAST(Id AS VARCHAR) = @Explain
INSERT INTO dbo.AppRefLet
    (Candidate,UserId, hosp, official, title, let_type, alternaterole, explain, email, phone, city, state,exam_type,sec_order,id_code,let_sent)
VALUES
    (@Candidate,@UserId, @Hosp, @Official, @Title, @RoleId, @AltRoleId, @ExplainText, @Email, @Phone, @City, @State,'',@SecOrder,@IdCode,GETDATE())
EXEC get_cc_refLetters @UserId
END
go
