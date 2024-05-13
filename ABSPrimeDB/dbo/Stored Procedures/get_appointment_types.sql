CREATE PROCEDURE [dbo].[get_appointment_types]
AS
	SELECT 
		Id,
		Name
	FROM
		appointment_types
	ORDER BY
		Name ASC