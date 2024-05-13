CREATE PROCEDURE [dbo].[get_accommodation_docs]
    @UserId INT
AS
    SELECT
        id,
        DocumentName
    FROM
        user_documents
    WHERE
        UserId = @UserId
        AND DocumentTypeId = 3
        AND InternalViewOnly = 0
