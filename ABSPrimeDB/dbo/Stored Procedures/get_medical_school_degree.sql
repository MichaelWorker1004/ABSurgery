CREATE PROCEDURE [dbo].[get_medical_school_degree]
AS
	SELECT degree_id, Description from degree
RETURN 0
