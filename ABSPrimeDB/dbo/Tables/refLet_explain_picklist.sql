CREATE TABLE [dbo].[refLet_explain_picklist] (
    [Id] INT NOT NULL IDENTITY(0,1) PRIMARY KEY,
    [Explain] VARCHAR(200) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
)