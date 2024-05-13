CREATE PROCEDURE [dbo].[get_qe_attestation_items_by_userId]
	@UserId INT,
	@ExamId INT
AS
    DECLARE @ExamTypeId INT,@ExamSpecialtyId INT
    SELECT @ExamTypeId = ExamTypeId, @ExamSpecialtyId = ExamSpecialtyId FROM Exam_Directory WHERE Id = @ExamId
    SELECT
        attestations_text.AttestationText
        , User_Profiles.LastName+', '+User_Profiles.FirstName+' '+User_Profiles.MiddleName  Name
        ,IIF(ISNULL(sig_receive, '')='', 0, 1) Checked
    FROM track
    LEFT JOIN exam_directory ON exam_directory.Id=@ExamId
    LEFT JOIN User_Profiles on User_Profiles.UserId=@UserId
    LEFT JOIN attestations_text ON attestations_text.ExamTypeId = @ExamTypeId AND attestations_text.ExamSpecialtyId = @ExamSpecialtyId
    WHERE track.UserId=@UserId and CAST(track.year AS VARCHAR)+track.exam+track.type=exam_directory.examcode