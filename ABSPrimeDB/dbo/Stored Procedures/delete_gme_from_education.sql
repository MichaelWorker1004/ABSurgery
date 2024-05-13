CREATE PROCEDURE [dbo].[delete_gme_from_education]
    @RefId INT
AS
    DELETE
    FROM
        education
    WHERE
        refId = @RefId
