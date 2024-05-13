CREATE TABLE [dbo].[certificate_types]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY
	,[Name] VARCHAR(50) NOT NULL
	,[Description] VARCHAR(150) NULL
	,[CreatedByUserId] INT NOT NULL
	,[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedByUserId] INT NOT NULL
)
GO
CREATE UNIQUE INDEX [IX_certificate_types] ON [dbo].[certificate_types] ([Name])
