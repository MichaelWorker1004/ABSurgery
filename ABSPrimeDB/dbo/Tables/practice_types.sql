CREATE TABLE [dbo].[practice_types]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(100) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
)