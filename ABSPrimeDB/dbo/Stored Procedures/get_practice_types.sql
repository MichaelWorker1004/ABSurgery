CREATE PROCEDURE [dbo].[get_practice_types]
AS
	SELECT 
		Id,
		Name
	FROM
		practice_types
	ORDER BY
		Name ASC