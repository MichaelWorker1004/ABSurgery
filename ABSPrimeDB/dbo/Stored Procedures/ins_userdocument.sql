CREATE PROCEDURE [dbo].[ins_userdocument]
	@UserId INT,
	@StreamId UNIQUEIDENTIFIER,
	@DocumentName VARCHAR(50),
	@DocumentTypeId INT,
	@InternalViewOnly BIT,
	@CreatedByUserId INT
AS
	INSERT INTO
		user_documents
		(
			UserId,
			StreamId,
			DocumentName,
			DocumentTypeId,
			InternalViewOnly,
			CreatedByUserId,
			LastUpdatedByUserId
		)
		VALUES
		(
			@UserId,
			@StreamId,
			@DocumentName,
			@DocumentTypeId,
			@InternalViewOnly,
			@CreatedByUserId,
			@CreatedByUserId
		)
	DECLARE @Id INT
	SET @Id = @@IDENTITY

	EXEC get_document_byid @Id, @UserId
