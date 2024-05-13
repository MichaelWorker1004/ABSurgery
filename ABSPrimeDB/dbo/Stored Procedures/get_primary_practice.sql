CREATE PROCEDURE [dbo].[get_primary_practice]
AS
    SELECT
        Id,
        Practice
    FROM
        Primary_Practice