CREATE PROCEDURE [dbo].[delete_outcome_registries]
    @UserId INT
AS
	SET NOCOUNT ON
	DELETE FROM  [dbo].[outcome_registries]
	WHERE
		UserId= @UserId

RETURN 0
