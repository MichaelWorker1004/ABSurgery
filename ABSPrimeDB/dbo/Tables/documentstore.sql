CREATE TABLE [dbo].[documentstore] (
    [stream_id]     UNIQUEIDENTIFIER NOT NULL,
    [UserId]        INT              NULL,
    [candidateid]   NVARCHAR (50)    NULL,
    [name]          NVARCHAR (255)   NOT NULL,
    [ip]            NVARCHAR (255)   NOT NULL,
    [file_type]     NVARCHAR (255)   NULL,
    [file_size]     BIGINT           NULL,
    [creation_time] DATETIME         CONSTRAINT [DF__documents__creat__72D1C499] DEFAULT (getdate()) NULL,
    [is_delete]     BIT              NULL,
    [note]          VARCHAR (255)    NULL,
    [verified]      DATETIME         NULL,
    CONSTRAINT [FK_documentstore_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);