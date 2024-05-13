CREATE TABLE [dbo].[caselog] (
    [id]          NUMERIC (18)     IDENTITY (1, 1) NOT NULL,
    [created]     [dbo].[created]  NOT NULL,
    [modified]    [dbo].[modified] NULL,
    [UserId]      INT              NULL,
    [candidate]   CHAR (6)         NOT NULL,
    [patient_id]  VARCHAR (20)     NOT NULL,
    [patient_age] VARCHAR (50)     NULL,
    [days_icu]    VARCHAR (5)      NULL,
    [categories]  VARCHAR (8000)   NULL,
    [procedures]  VARCHAR (8000)   NULL,
    PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [FK_caselog_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [CI_Candidate]
    ON [dbo].[caselog]([candidate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER tr_caselog ON dbo.caselog
FOR  UPDATE
AS
UPDATE caselog SET modified=getdate() WHERE id=(SELECT id FROM inserted)