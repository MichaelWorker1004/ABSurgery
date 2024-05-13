CREATE TABLE [dbo].[biodata] (
    [created]     [dbo].[created]  NOT NULL,
    [modified]    [dbo].[modified] NULL,
    [UserId]      INT              NULL,
    [candidate]   CHAR (6)         NULL,
    [image]       IMAGE            NULL,
    [type]        VARCHAR (4)      NULL,
    [filename]    VARCHAR (50)     NULL,
    [capturedate] DATETIME         NULL,
    CONSTRAINT [FK_biodata_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_CandidateType]
    ON [dbo].[biodata]([candidate] ASC, [type] ASC) WITH (FILLFACTOR = 90);

