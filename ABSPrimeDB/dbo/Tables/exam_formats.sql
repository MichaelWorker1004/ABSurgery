CREATE TABLE [dbo].[exam_formats]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY
    ,[Code] VARCHAR(1) NULL
	,[Name] VARCHAR(50) NOT NULL
	,[CreatedByUserId] INT NOT NULL
	,[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedByUserId] INT NOT NULL
)
GO