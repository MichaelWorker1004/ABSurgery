CREATE PROCEDURE [dbo].[delete_examinerscore_byid]
	@ExamScoringId INT
AS
	DELETE FROM
		exam_scoring
	WHERE
		Id = @ExamScoringId
