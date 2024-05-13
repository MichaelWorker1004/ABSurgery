CREATE TABLE [dbo].[Primary_Practice]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Practice] VARCHAR(100) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
)