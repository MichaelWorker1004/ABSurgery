CREATE TABLE [dbo].[user_tokens]
(
    [UserId] INT NOT NULL,
    [Token] VARCHAR(50) NOT NULL,
    [ExpiresAt] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_user_tokens_users] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId])
 );
GO

CREATE UNIQUE CLUSTERED INDEX user_tokens_token ON [dbo].[user_tokens] ([Token])
GO