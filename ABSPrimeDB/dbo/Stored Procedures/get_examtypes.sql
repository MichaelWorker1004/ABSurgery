CREATE PROCEDURE [dbo].[get_examtypes]
AS
	SELECT 
		Id,
		Code
	FROM
		[dbo].[exam_types]
	ORDER BY
		[Name]