CREATE PROCEDURE [dbo].[get_refLet_explain_picklist]
AS
    SELECT
        [Id],
        [Explain]
    FROM
            [dbo].[refLet_explain_picklist]