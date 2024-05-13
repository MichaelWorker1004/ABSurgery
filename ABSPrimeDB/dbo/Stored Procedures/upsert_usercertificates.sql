CREATE PROCEDURE [dbo].[upsert_usercertificates]
	@UserId INT,
	@DocumentId INT,
	@CertificateTypeId INT,
	@IssueDate DATETIME,
	@CertificateNumber NVARCHAR(50),
	@CreatedByUserId INT
AS
    DECLARE @CertificateId INT
    SET @CertificateId = (SELECT top 1 (Id) FROM user_certificates WHERE UserId = @UserId AND CertificateTypeId = @CertificateTypeId ORDER BY Id DESC)
    IF(@CertificateId IS NOT NULL) BEGIN
        UPDATE
            dbo.user_certificates
        SET
            UserId = @UserId,
            DocumentId = @DocumentId,
            CertificateTypeId = @CertificateTypeId,
            IssueDate = @IssueDate,
            CertificateNumber = @CertificateNumber,
			LastUpdatedAtUtc = GetUtcDate(),
            LastUpdatedByUserId = @CreatedByUserId
        WHERE
            Id = @CertificateId
    END
    ELSE BEGIN
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
	SET @CertificateId = SCOPE_IDENTITY()
    END
	EXEC dbo.get_usercertificates_byid @CertificateId
go

