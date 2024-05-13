CREATE TABLE [dbo].[case_headers]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Title] VARCHAR(MAX) NULL,
    [Description] VARCHAR(MAX) NULL,
    [CaseNumber] VARCHAR(20) NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL 
)