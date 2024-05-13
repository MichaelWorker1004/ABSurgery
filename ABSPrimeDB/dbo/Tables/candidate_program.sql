CREATE TABLE [dbo].[candidate_program] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [UserId]        INT                   NULL,
    [candidate]     CHAR (6)              NOT NULL,
    [exam]          [dbo].[exam]          NOT NULL,
    [compyear]      SMALLINT              NOT NULL,
    [program]       CHAR (4)              NOT NULL,
    [rtype]         TINYINT               NULL,
    [pass_fail]     CHAR (1)              NULL,
    [remedial]      CHAR (1)              NULL,
    [note]          VARCHAR (255)         NULL,
    [actcode]       SMALLINT              NULL,
    [ID]            INT                   IDENTITY (4050, 1) NOT NULL,
    PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_candidate_program_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE UNIQUE CLUSTERED INDEX [candidate_program_uc]
    ON [dbo].[candidate_program]([candidate] ASC, [exam] ASC, [compyear] ASC, [program] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_16_15_candidate_program]
    ON [dbo].[candidate_program]([compyear] ASC)
    INCLUDE([candidate], [exam]);


GO
CREATE NONCLUSTERED INDEX [IX_candidate_program_compyear_Inc_candidate_exam]
    ON [dbo].[candidate_program]([compyear] ASC)
    INCLUDE([candidate], [exam]);

