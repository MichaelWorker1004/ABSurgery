CREATE TABLE [dbo].[case_contents]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [CaseHeaderId] INT NOT NULL,
    [Heading] VARCHAR(MAX) NULL,
    [Content] VARCHAR(MAX) NULL,
    [SectionNumber] INT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_case_contents_caseheaderid] FOREIGN KEY ([CaseHeaderId]) REFERENCES [case_headers]([Id])
)