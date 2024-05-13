CREATE PROCEDURE [dbo].[get_examspecialties]
AS
	SELECT 
		Id,
		Name
	FROM
		[dbo].[exam_specialties]
	ORDER BY
		[Name]
