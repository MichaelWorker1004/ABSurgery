CREATE TABLE [dbo].[user_case_comments]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserId] INT NOT NULL,
    [CaseContentId] INT NOT NULL,
    [Comments] VARCHAR(5000) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_user_case_comments_userid] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_user_case_comments_casecontentid] FOREIGN KEY ([CaseContentId]) REFERENCES [case_contents]([Id])
)