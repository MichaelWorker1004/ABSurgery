CREATE PROCEDURE [dbo].[get_fellowship_types]
AS
SELECT
    FellowshipType,FellowshipTypeName
FROM
    fellowship_types_picklist
