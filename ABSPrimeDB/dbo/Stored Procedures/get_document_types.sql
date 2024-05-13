CREATE PROCEDURE [dbo].[get_document_types]
AS
	SELECT 
		Id,
		Name
	FROM
		document_types
	ORDER BY
		Name ASC
