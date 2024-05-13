CREATE TABLE [dbo].[lapsed_pathway_types]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL,
	[CreatedByUserId] INT NOT NULL,
	[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LastUpdatedByUserId] INT NOT NULL
)
