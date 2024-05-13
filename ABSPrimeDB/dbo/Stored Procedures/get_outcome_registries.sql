CREATE PROCEDURE [dbo].[get_outcome_registries]
	@UserID INT
AS
	SELECT
	    *
	FROM outcome_registries
	WHERE userid = @UserID
RETURN 0
