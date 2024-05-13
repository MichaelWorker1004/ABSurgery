CREATE PROCEDURE [dbo].[get_examstatus]
AS
	SELECT 
		Id,
		Name
	FROM
		[dbo].[exam_statuses]
	ORDER BY
		[Name]
