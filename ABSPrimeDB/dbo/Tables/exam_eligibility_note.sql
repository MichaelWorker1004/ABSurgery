CREATE TABLE [dbo].[exam_eligibility_note] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [UserId]        INT                   NULL,
    [candidate]     CHAR (6)              NOT NULL,
    [note]          VARCHAR (8000)        NULL,
    CONSTRAINT [FK_exam_eligibility_note_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE UNIQUE CLUSTERED INDEX [exam_eligibility_note_uc]
    ON [dbo].[exam_eligibility_note]([candidate] ASC) WITH (FILLFACTOR = 90);

