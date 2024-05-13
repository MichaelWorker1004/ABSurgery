CREATE PROCEDURE [dbo].[get_accommodation_picklist]
AS
    SELECT
        [AccommodationId] AS ID,
        [Code],
        [DocumentLink]
    FROM
        [dbo].[AccommodationsId]
    ORDER BY
        [AccommodationId]