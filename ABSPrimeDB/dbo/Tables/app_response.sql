CREATE TABLE [dbo].[app_response] (
    [UserId]    INT           NULL,
    [candidate] VARCHAR (6)   NULL,
    [exam_type] VARCHAR (8)   NULL,
    [app_id]    TINYINT       NULL,
    [response]  TEXT          NULL,
    [status]    TINYINT       NULL,
    [note]      VARCHAR (255) NULL,
    [version]   TINYINT       NULL,
    [last_updt] SMALLDATETIME NULL,
    [id]        NUMERIC (9)   IDENTITY (1, 1) NOT NULL,
    [approved]  SMALLDATETIME NULL,
    CONSTRAINT [PK_app_response] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_app_response_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [uc_cand_type_id]
    ON [dbo].[app_response]([candidate] ASC, [exam_type] ASC, [app_id] ASC) WITH (FILLFACTOR = 90);

