CREATE PROCEDURE [dbo].[get_picklist_languages_all]
AS
	SELECT
		LanguageId as ItemValue,
		Name as ItemDescription
	FROM
		[dbo].[languages]
