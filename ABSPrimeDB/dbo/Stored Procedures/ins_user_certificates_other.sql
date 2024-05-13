CREATE PROCEDURE [dbo].[ins_user_certificates_other]
    @UserId int,
    @CertificateTypeId int,
    @IssueDate datetime,
    @CertificateNumber varchar(25),
    @CreatedByUserId int
AS
    INSERT INTO [dbo].[user_certificates_other]
           ([UserId]
           ,[CertificateTypeId]
           ,[IssueDate]
           ,[CertificateNumber]
           ,[CreatedByUserId]
           ,[CreatedAtUtc]
           ,[LastUpdatedAtUtc]
           ,[LastUpdatedByUserId])
     VALUES
           (@UserId
           ,@CertificateTypeId
           ,@IssueDate
           ,@CertificateNumber
           ,@CreatedByUserId
           ,getutcdate()
           ,getutcdate()
           ,@CreatedByUserId)
DECLARE @Id INT
SET @Id = @@IDENTITY
EXEC [dbo].[get_user_certificates_other_byid] @Id