CREATE PROCEDURE [dbo].[get_picklists_ethnicities_all]
AS
 SELECT 
		Code as ItemValue,
		Descr as ItemDescription
	FROM 
		mcodes 
	where 
		grp = 'et'