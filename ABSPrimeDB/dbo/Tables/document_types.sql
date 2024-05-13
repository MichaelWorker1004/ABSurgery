CREATE TABLE [dbo].[document_types]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(500) NULL,
	[CreatedByUserId] INT NOT NULL,
	[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LastUpdatedByUserId] INT NOT NULL
)
GO
CREATE UNIQUE INDEX [IX_document_types_name] ON [dbo].[document_types] ([Name])
