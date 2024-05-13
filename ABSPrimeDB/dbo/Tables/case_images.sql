CREATE TABLE [dbo].[case_images]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [CaseHeaderId] INT NOT NULL,
    [Name] VARCHAR(1000) NOT NULL,
    [SortOrder] INT NULL,
    [Image] VARCHAR(MAX) NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_case_images_caseheaderid] FOREIGN KEY ([CaseHeaderId]) REFERENCES [case_headers]([Id])
)