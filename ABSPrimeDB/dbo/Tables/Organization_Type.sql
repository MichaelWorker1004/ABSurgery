CREATE TABLE [dbo].[Organization_Type]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Type] NVARCHAR(100) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
)