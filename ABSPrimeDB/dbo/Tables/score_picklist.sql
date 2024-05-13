CREATE TABLE [dbo].[score_picklist]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Score] INT NOT NULL,
    [Result] VARCHAR(20) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
)