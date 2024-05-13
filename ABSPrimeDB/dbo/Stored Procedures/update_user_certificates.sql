CREATE PROCEDURE [dbo].[update_user_certificates]
	@Id INT,
	@UserId INT,
	@Description varchar(150),
	@CertificateTypeId INT,
	@IssueDate DATE,
	@CertificateNumber varchar(25),
	@LastUpdatedByUserId INT
AS
	UPDATE user_certificates
	SET
		UserId = @UserId,
		Description = @Description,
		CertificateTypeId = @CertificateTypeId,
		IssueDate = @IssueDate,
		CertificateNumber = @CertificateNumber,
		LastUpdatedAtUtc = GetUtcDate(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		Id = @Id

	EXEC [dbo].[get_user_certificates_byuserid] @UserId
RETURN 0
