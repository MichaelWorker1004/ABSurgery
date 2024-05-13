CREATE TABLE [dbo].[user_certificates_other]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [int] NOT NULL,
    [CertificateTypeId] [int] NOT NULL,
    [IssueDate] [date] NOT NULL,
    [CertificateNumber] [varchar](25) NOT NULL,
    [CreatedByUserId] [int] NOT NULL,
    [CreatedAtUtc] [datetime] NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] [datetime] NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] [int] NOT NULL
    CONSTRAINT [FK_user_certificates_other] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]),
    CONSTRAINT [FK_certificate_types_other] FOREIGN KEY ([CertificateTypeId]) REFERENCES [certificate_types]([Id]),
)