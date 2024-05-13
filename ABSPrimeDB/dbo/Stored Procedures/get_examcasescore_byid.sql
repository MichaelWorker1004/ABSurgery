CREATE PROCEDURE [dbo].[get_examcasescore_byid]
	@ExamScoringId INT,
    @ExaminerUserId INT 
AS
	SELECT
		esco.Id as ExamScoringId,
		esco.ExamCaseId,
		esco.ExaminerUserId,
		esco.ExamineeUserId,
		up.FirstName ExamineeFirstName,
        up.MiddleName ExamineeMiddleName,
        up.LastName ExamineeLastName,
        up.Suffix ExamineeSuffix,
		Score,
		CriticalFail,
		Remarks,
		--Submitted,
		--SubmittedDateTimeUTC,
		esco.CreatedByUserId,
		esco.CreatedAtUtc,
		esco.LastUpdatedAtUtc,
		esco.LastUpdatedByUserId
	FROM
		exam_scoring esco
		INNER JOIN user_profiles up ON up.UserId=esco.ExamineeUserId
	WHERE
		esco.Id = @ExamScoringId and
		esco.ExaminerUserId = @ExaminerUserId

