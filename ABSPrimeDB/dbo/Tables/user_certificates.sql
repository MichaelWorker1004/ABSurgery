CREATE TABLE [dbo].[user_certificates]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [DocumentId] INT NOT NULL,
    [CertificateTypeId] INT NOT NULL, 
    [IssueDate] DATE NOT NULL, 
    [CertificateNumber] VARCHAR(25) NOT NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_user_certificates] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]),
    CONSTRAINT [FK_certificate_types] FOREIGN KEY ([CertificateTypeId]) REFERENCES [certificate_types]([Id]), 
    CONSTRAINT [FK_user_certificates_documentid] FOREIGN KEY ([DocumentId]) REFERENCES [user_documents]([Id])
)
