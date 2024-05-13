CREATE PROCEDURE [dbo].[update_userfellowships]
	@Id INT,
	@UserId INT,
	@ProgramName VARCHAR(500),
	@CompletionYear CHAR(4),
	@ProgramOther VARCHAR(500),
	@LastUpdatedByUserId INT
AS
	UPDATE
		user_fellowships
	SET
		UserId = @UserId,
		ProgramName = @ProgramName,
		CompletionYear = @CompletionYear,
		ProgramOther = @ProgramOther,
		LastUpdatedAtUtc = GetUtcDate(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @Id

	EXEC [dbo].[get_userfellowships_byid] @Id

RETURN 0
