CREATE TABLE [dbo].[applog] (
    [UserId]      INT           NULL,
    [candidate]   VARCHAR (100) NULL,
    [exam_type]   VARCHAR (8)   NULL,
    [app_id]      TINYINT       NULL,
    [app_section] VARCHAR (100) NULL,
    [begin_time]  DATETIME      NULL,
    [end_time]    DATETIME      NULL,
    [ip]          VARCHAR (15)  NULL,
    [diff_time]   INT           NULL,
    [server_name] VARCHAR (50)  NULL,
    [callouttime] SMALLINT      NULL,
    CONSTRAINT [FK_applog_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_applog_1]
    ON [dbo].[applog]([candidate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_applog]
    ON [dbo].[applog]([begin_time] ASC) WITH (FILLFACTOR = 90);

