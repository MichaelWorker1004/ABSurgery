CREATE PROCEDURE [dbo].[get_userfellowships_byid]
	@Id INT
AS
	SELECT 
		Id,
		UserId,
		ProgramName,
		CompletionYear,
		ProgramOther,
		CreatedByUserId,
		CreatedAtUtc,
		LastUpdatedAtUtc,
		LastUpdatedByUserId
	FROM
		user_fellowships
	WHERE
		Id = @Id

RETURN 0
