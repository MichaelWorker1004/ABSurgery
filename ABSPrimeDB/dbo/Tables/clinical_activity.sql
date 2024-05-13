CREATE TABLE [dbo].[clinical_activity]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	,[Name] VARCHAR(50) NOT NULL
	,[IsActive] BIT NOT NULL
	,IsEssential BIT NOT NULL DEFAULT 0
	,IsCredit BIT NOT NULL DEFAULT 0
	,[CreatedByUserId] INT NOT NULL
    ,[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
    ,[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
    ,[LastUpdatedByUserId] INT NOT NULL
)
