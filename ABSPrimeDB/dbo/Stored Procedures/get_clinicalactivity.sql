CREATE PROCEDURE [dbo].[get_clinicalactivity]
AS
	SELECT
		Id,
		Name,
		IsCredit,
		IsEssential
	FROM
		clinical_activity
	WHERE
		IsActive = 1

RETURN 0
