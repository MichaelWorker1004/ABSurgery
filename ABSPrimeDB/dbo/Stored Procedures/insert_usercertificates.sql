CREATE PROCEDURE [dbo].[insert_usercertificates]
	@UserId INT,
	@DocumentId INT,
	@CertificateTypeId INT,
	@IssueDate DATETIME,
	@CertificateNumber NVARCHAR(50),
	@CreatedByUserId INT
AS
	INSERT INTO
		dbo.user_certificates
		(
			UserId,
			DocumentId,
			CertificateTypeId,
			IssueDate,
			CertificateNumber,
			CreatedByUserId,
			LastUpdatedByUserId
		)
		VALUES
		(
			@UserId,
			@DocumentId,
			@CertificateTypeId,
			@IssueDate,
			@CertificateNumber,
			@CreatedByUserId,
			@CreatedByUserId
		)
	DECLARE @CertificateId int
	SET @CertificateId = SCOPE_IDENTITY()

	EXEC dbo.get_usercertificates_byid @CertificateId