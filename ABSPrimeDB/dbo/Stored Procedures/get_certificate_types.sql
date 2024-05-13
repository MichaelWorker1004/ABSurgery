CREATE PROCEDURE [dbo].[get_certificate_types]
AS
	SELECT 
		Id,
		Name
	FROM
		certificate_types
	ORDER BY
		Name ASC

