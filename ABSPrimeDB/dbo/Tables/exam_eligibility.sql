CREATE TABLE [dbo].[exam_eligibility] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [UserId]        INT                   NULL,
    [candidate]     CHAR (6)              NOT NULL,
    [exam]          [dbo].[exam]          NOT NULL,
    [type]          CHAR (1)              NOT NULL,
    [year_start]    SMALLINT              NULL,
    [year_end]      SMALLINT              NULL,
    [examstaken]    TINYINT               NULL,
    [admissible]    CHAR (1)              NULL,
    [ID]            INT                   IDENTITY (4321, 1) NOT NULL,
    PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_exam_eligibility_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE UNIQUE CLUSTERED INDEX [exam_eligibility_uc]
    ON [dbo].[exam_eligibility]([candidate] ASC, [exam] ASC, [type] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_25_24_exam_eligibility]
    ON [dbo].[exam_eligibility]([admissible] ASC, [type] ASC)
    INCLUDE([candidate], [exam]);


GO
CREATE NONCLUSTERED INDEX [IX_10_9_exam_eligibility]
    ON [dbo].[exam_eligibility]([admissible] ASC)
    INCLUDE([candidate], [exam], [type], [year_start], [year_end]);


GO
CREATE NONCLUSTERED INDEX [IX_26996_26995_exam_eligibility]
    ON [dbo].[exam_eligibility]([admissible] ASC)
    INCLUDE([candidate], [exam], [type], [year_start], [year_end]);


GO
CREATE TRIGGER [dbo].[tr_exam_eligibility_update] ON dbo.exam_eligibility
FOR  UPDATE ,DELETE
AS
insert into exam_eligibility_hist select * from deleted