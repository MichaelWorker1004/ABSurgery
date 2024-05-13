CREATE PROCEDURE [dbo].[get_picklist_genders_all]
AS
	SELECT
		GenderId as ItemValue,
		Name as ItemDescription
	FROM
		[dbo].[genders]

