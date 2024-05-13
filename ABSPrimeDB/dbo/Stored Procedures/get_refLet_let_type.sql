CREATE PROCEDURE [dbo].[get_refLet_let_type]
AS
 SELECT
    [Id],
    [Role]
 FROM
        [dbo].[refLet_let_type_picklist]
