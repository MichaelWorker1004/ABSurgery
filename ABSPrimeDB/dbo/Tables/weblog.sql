CREATE TABLE [dbo].[weblog] (
    [UserId]    INT            NULL,
    [candidate] VARCHAR (50)   NULL,
    [ip]        VARCHAR (15)   NULL,
    [uri]       VARCHAR (100)  NULL,
    [brw]       VARCHAR (300)  NULL,
    [logtime]   VARCHAR (25)   NULL,
    [params]    VARCHAR (1000) NULL,
    [created]   DATETIME       DEFAULT (getdate()) NULL,
    CONSTRAINT [FK_weblog_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_weblog]
    ON [dbo].[weblog]([candidate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_created]
    ON [dbo].[weblog]([created] ASC) WITH (FILLFACTOR = 90);

