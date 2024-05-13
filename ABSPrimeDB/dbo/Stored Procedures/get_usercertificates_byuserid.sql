CREATE PROCEDURE [dbo].[get_usercertificates_byuserid]
	@UserId INT
AS
	SELECT
		uc.Id as CertificateId,
		uc.UserId,
		uc.DocumentId,
		uc.CertificateTypeId,
		ct.Name as CertificateType,
		uc.IssueDate,
		uc.CertificateNumber,
		ud.DocumentName,
		uc.LastUpdatedAtUtc as UploadDateUtc
	FROM
		dbo.user_certificates uc
		inner join dbo.certificate_types ct on ct.id = uc.CertificateTypeId
		inner join dbo.user_documents ud on ud.id = uc.DocumentId
	WHERE
		uc.UserId = @UserId

