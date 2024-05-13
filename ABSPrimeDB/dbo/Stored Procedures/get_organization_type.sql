CREATE PROCEDURE [dbo].[get_organization_type]
AS
    SELECT
        Id,
        Type
    FROM
        Organization_Type
RETURN 0