CREATE PROCEDURE [dbo].[update_qe_attestation_by_userid]
	@UserId INT,
	@ExamId INT
AS
    DECLARE @ExamCode VARCHAR(10)
    SELECT @ExamCode = ExamCode FROM Exam_Directory WHERE Id = @ExamId
    UPDATE
        track
    SET
        sig_receive=GETUTCDATE()
        ,modifier='dbo'
        ,modified=GETUTCDATE()
        ,modifications=modifications+1
    WHERE
        track.UserId=@UserId and CAST(track.year AS VARCHAR)+track.exam+track.type=@ExamCode
EXEC get_qe_attestation_items_by_userId @UserId, @ExamId