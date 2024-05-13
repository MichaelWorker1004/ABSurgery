CREATE PROCEDURE [dbo].[get_picklist_country_all]
AS
  SELECT 
	[code] as ItemValue
   ,[description] as ItemDescription
  FROM 
	[dbo].[country]
  ORDER BY 
	sort_code, 
	[description] ASC
