CREATE TABLE [dbo].[AppReplyCardsTraining] (
    [id]        NUMERIC (9)   IDENTITY (1, 1) NOT NULL,
    [created]   SMALLDATETIME NULL,
    [UserId]    INT           NULL,
    [candidate] CHAR (6)      NULL,
    [q216]      VARCHAR (50)  NULL,
    [q28]       VARCHAR (50)  NULL,
    [q213]      VARCHAR (100) NULL,
    [q212]      VARCHAR (100) NULL,
    [q32]       VARCHAR (255) NULL,
    [q19]       VARCHAR (10)  NULL,
    [q20]       VARCHAR (10)  NULL,
    CONSTRAINT [FK_appreplycardstraining_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AppReplyCardsTraining]
    ON [dbo].[AppReplyCardsTraining]([candidate] ASC) WITH (FILLFACTOR = 90);

