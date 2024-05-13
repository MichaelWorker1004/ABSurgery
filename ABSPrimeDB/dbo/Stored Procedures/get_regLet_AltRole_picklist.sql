CREATE PROCEDURE [dbo].[get_regLet_AltRole_picklist]
AS
 SELECT
    [Id],
    [Role]
 FROM
        [dbo].[refLet_altRole_picklist]