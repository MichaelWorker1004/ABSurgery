CREATE PROCEDURE [dbo].[get_user_certificates_other_byuserid]
    @UserId int
AS
    SELECT
        uco.Id,
        uco.UserId,
        uco.CertificateTypeId,
        ct.Name AS CertificateTypeName,
        uco.IssueDate,
        uco.CertificateNumber,
        uco.CreatedByUserId,
        uco.CreatedAtUtc,
        uco.LastUpdatedAtUtc,
        uco.LastUpdatedByUserId
    FROM
        [dbo].[user_certificates_other] uco
    join certificate_types ct on  uco.CertificateTypeId = ct.Id
    WHERE
        uco.UserId = @UserId
RETURN 0
