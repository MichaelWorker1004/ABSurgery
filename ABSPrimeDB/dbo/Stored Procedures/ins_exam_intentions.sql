CREATE PROCEDURE [dbo].[ins_exam_intentions]
    @UserId INT,
    @ExamId INT,
    @Intention BIT
AS
    UPDATE
        exam
    SET
        exam.rply_rcvd = GETUTCDATE(),
        exam.istaking = @Intention
    WHERE
        exam.UserId = @UserId
        AND exam.examHeaderId = @ExamId
    EXEC get_exam_intentions @UserId, @ExamId