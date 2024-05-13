CREATE PROCEDURE [dbo].[get_user_certificates_byuserid]
	@UserId INT
AS
	SELECT 
		uc.Id,
		uc.UserId,
		uc.Description,
		uc.CertificateTypeId,
		ct.Name AS CertificateTypeName,
		uc.IssueDate,
		uc.CertificateNumber,
		uc.CreatedByUserId,
		uc.CreatedAtUtc,
		uc.LastUpdatedAtUtc,
		uc.LastUpdatedByUserId
	FROM
		user_certificates uc
		INNER JOIN certificate_types ct ON ct.Id = uc.CertificateTypeId
	WHERE
		uc.UserId = @UserId
	ORDER BY
		uc.IssueDate DESC

RETURN 0
