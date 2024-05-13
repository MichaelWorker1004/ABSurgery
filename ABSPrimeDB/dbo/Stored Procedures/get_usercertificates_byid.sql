CREATE PROCEDURE [dbo].[get_usercertificates_byid]
	@CertificateId INT
AS
	SELECT 
		uc.Id as CertificateId,
		UserId,
		DocumentId,
		CertificateTypeId,
		ct.Name as CertificateType,
		IssueDate,
		CertificateNumber,
		uc.CreatedByUserId,
		uc.CreatedAtUtc,
		uc.LastUpdatedAtUtc,
		uc.LastUpdatedByUserId
	FROM
		dbo.user_certificates uc
		inner join dbo.certificate_types ct on ct.id = uc.CertificateTypeId
	WHERE
		uc.Id = @CertificateId
