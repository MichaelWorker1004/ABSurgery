CREATE TABLE [dbo].[clinical_level]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[Name] VARCHAR(50) NOT NULL
	,[ClinicalLevel] tinyint NOT NULL DEFAULT 0
	,[IsActive] BIT NOT NULL
	,[CreatedByUserId] INT NOT NULL
    ,[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
    ,[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
    ,[LastUpdatedByUserId] INT NOT NULL
)
