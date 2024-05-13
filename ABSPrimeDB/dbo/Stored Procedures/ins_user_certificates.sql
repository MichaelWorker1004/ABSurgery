CREATE PROCEDURE [dbo].[ins_user_certificates]
	@UserId INT,
	@Description varchar(150),
	@CertificateTypeId INT,
	@IssueDate DATE,
	@CertificateNumber varchar(25),
	@CreatedByUserId INT
AS
	INSERT INTO user_certificates
	(
		UserId,
		Description,
		CertificateTypeId,
		IssueDate,
		CertificateNumber,
		CreatedByUserId,
		LastUpdatedByUserId
	)
	VALUES
(
		@UserId,
		@Description,
		@CertificateTypeId,
		@IssueDate,
		@CertificateNumber,
		@CreatedByUserId,
		@CreatedByUserId
	)

	EXEC [dbo].[get_user_certificates_byuserid] @UserId

RETURN 0
