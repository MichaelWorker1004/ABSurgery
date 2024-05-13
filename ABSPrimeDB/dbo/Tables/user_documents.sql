CREATE TABLE [dbo].[user_documents]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [StreamId] UNIQUEIDENTIFIER NOT NULL, 
    [DocumentName] VARCHAR(255) NOT NULL,
    [DocumentTypeId] INT NOT NULL, 
    [InternalViewOnly] BIT NOT NULL,
    [is_delete] BIT NULL,
    [note] VARCHAR (255) NULL,
    [verified] DATETIME NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_user_documents] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]), 
)
