CREATE TABLE [dbo].[survey] (
    [created]   [dbo].[created] NULL,
    [UserId]    INT             NULL,
    [candidate] CHAR (6)        NULL,
    [qid]       VARCHAR (50)    NULL,
    [qtext]     VARCHAR (1000)  NULL,
    [campaign]  VARCHAR (50)    NULL,
    CONSTRAINT [FK_survey_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [IX_survey]
    ON [dbo].[survey]([candidate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_survey_1]
    ON [dbo].[survey]([qid] ASC) WITH (FILLFACTOR = 90);

