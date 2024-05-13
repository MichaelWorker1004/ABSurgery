CREATE PROCEDURE [dbo].[update_usercertificates]
	@CertificateId INT,
	@UserId INT,
	@DocumentId INT,
	@CertificateTypeId INT,
	@IssueDate DATETIME,
	@CertificateNumber NVARCHAR(50),
	@LastUpdatedByUserId INT
AS
	UPDATE
		dbo.user_certificates
	SET
		UserId = @UserId,
		DocumentId = @DocumentId,
		CertificateTypeId = @CertificateTypeId,
		IssueDate = @IssueDate,
		CertificateNumber = @CertificateNumber,
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @CertificateId

	EXEC dbo.get_usercertificates_byid @CertificateId