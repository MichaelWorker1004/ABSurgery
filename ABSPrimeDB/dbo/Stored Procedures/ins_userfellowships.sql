CREATE PROCEDURE [dbo].[ins_userfellowships]
	@UserId INT,
	@ProgramName VARCHAR(500) = NULL,
	@CompletionYear CHAR(4),
	@ProgramOther VARCHAR(500) = NULL,
	@CreatedByUserId INT
AS
	INSERT INTO 
		user_fellowships
	VALUES
	(@UserId, @ProgramName, @CompletionYear, @ProgramOther, @CreatedByUserId, GetUtcDate(), GetUtcDate(), @CreatedByUserId)

	DECLARE @Id INT
	SET @Id = SCOPE_IDENTITY()

	EXEC [dbo].[get_userfellowships_byid] @Id

RETURN 0
