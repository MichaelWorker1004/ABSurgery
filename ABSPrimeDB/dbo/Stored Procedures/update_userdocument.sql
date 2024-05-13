CREATE PROCEDURE [dbo].[update_userdocument]
	@Id INT,
	@UserId INT,
	@StreamId UNIQUEIDENTIFIER,
	@DocumentName VARCHAR(50),
	@DocumentTypeId INT,
	@InternalViewOnly BIT,
	@LastUpdatedByUserId INT
AS
	UPDATE
		dbo.user_documents
	SET
		UserId = @UserId,
		StreamId = @StreamId,
		DocumentName = @DocumentName,
		DocumentTypeId = @DocumentTypeId,
		InternalViewOnly = @InternalViewOnly,
		LastUpdatedAtUtc = GetUtcDate(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @Id

	EXEC get_document_byid @Id, @UserId