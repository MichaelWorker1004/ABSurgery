CREATE TABLE [dbo].[user_case_feedback]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [CaseHeaderId] INT NOT NULL,
    [feedback] VARCHAR(5000) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_user_case_feedback_userid] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_user_case_feedback_caseheaderid] FOREIGN KEY ([CaseHeaderId]) REFERENCES [case_headers]([Id])
)