CREATE PROCEDURE [dbo].[get_clinicallevel]
AS
	SELECT
		Id,
		Name
	FROM
		clinical_level
	WHERE
		IsActive = 1
RETURN 0
