CREATE TABLE [dbo].[user_fellowships]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[UserId] INT NOT NULL,
	[ProgramName] VARCHAR(500) NULL, 
    [CompletionYear] CHAR(4) NOT NULL, 
    [ProgramOther] VARCHAR(500) NULL,
	[CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_user_fellowships_userid] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId])
)
