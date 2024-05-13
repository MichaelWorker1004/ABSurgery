CREATE PROCEDURE [dbo].[get_picklist_races_all]
AS
	SELECT
		Code as ItemValue,
		Descr as ItemDescription
	FROM
		mcodes
	where
		grp = 'ra'
    ORDER BY
        IIF(Descr='I prefer not to answer','Z',Descr)