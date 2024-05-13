CREATE TABLE [dbo].[AppReplyCardsPractice] (
    [id]        NUMERIC (9)   IDENTITY (1, 1) NOT NULL,
    [created]   SMALLDATETIME NULL,
    [UserId]    INT           NULL,
    [candidate] CHAR (6)      NULL,
    [q215]      VARCHAR (50)  NULL,
    [q237]      VARCHAR (50)  NULL,
    [q105]      VARCHAR (50)  NULL,
    [q28]       VARCHAR (50)  NULL,
    [q213]      VARCHAR (100) NULL,
    [q212]      VARCHAR (100) NULL,
    [q36]       VARCHAR (100) NULL,
    [q32]       VARCHAR (255) NULL,
    [hosp_num]  TINYINT       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_appreplycardspractice_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AppReplyCardsPractice]
    ON [dbo].[AppReplyCardsPractice]([candidate] ASC) WITH (FILLFACTOR = 90);

