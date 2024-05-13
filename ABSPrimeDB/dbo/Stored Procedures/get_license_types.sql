CREATE PROCEDURE [dbo].[get_license_types]
AS
	SELECT 
		Id,
		Name
	FROM
		license_types
	ORDER BY
		Name ASC