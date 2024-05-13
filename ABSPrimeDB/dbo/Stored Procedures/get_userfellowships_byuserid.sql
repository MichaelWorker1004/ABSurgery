CREATE PROCEDURE [dbo].[get_userfellowships_byuserid]
	@UserId int
AS
	SELECT
	 Id,
	 ProgramName,
	 CompletionYear,
	 ProgramOther
	FROM
		user_fellowships
	WHERE
		UserId = @UserId

RETURN 0
