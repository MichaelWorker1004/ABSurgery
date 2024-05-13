CREATE PROCEDURE [dbo].[update_user_certificates_other_byid]
    @Id int,
    @CertificateTypeId int,
    @IssueDate datetime,
    @CertificateNumber varchar(25),
    @LastUpdatedByUserId int
AS
    UPDATE
        [dbo].[user_certificates_other]
    SET
        [CertificateTypeId] = @CertificateTypeId,
        [IssueDate] = @IssueDate,
        [CertificateNumber] = @CertificateNumber,
        [LastUpdatedByUserId] = @LastUpdatedByUserId,
        [LastUpdatedAtUtc] = GETUTCDATE()
    WHERE
        [Id] = @Id
EXEC [dbo].[get_user_certificates_other_byid] @Id
