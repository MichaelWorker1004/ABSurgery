CREATE TABLE [dbo].[license_types]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY
    ,[Code] VARCHAR(10) NULL
	,[Name] VARCHAR(50) NULL
	,[CreatedByUserId] INT NOT NULL
	,[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedByUserId] INT NOT NULL
)
GO
CREATE UNIQUE INDEX [IX_license_types] ON [dbo].[license_types] ([Name])