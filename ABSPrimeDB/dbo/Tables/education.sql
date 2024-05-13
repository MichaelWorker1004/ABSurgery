CREATE TABLE [dbo].[education] (
    [UserId]      INT           NULL,
    [candidate]   CHAR (6)      NOT NULL,
    [date_from]   DATE          NULL,
    [date_to]     DATE          NULL,
    [rot_type]    TINYINT       NULL,
    [subj_area]   VARCHAR (500) NULL,
    [prg_name]    VARCHAR (500) NULL,
    [clinical]    VARCHAR (500) NULL,
    [exam_type]   VARCHAR (6)   NULL,
    [institution] VARCHAR (500) NULL,
    [rot_intl]    VARCHAR (3)   NULL,
    [other]       VARCHAR (500) NULL,
    [id]          NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [refId]       INT           NULL,
    CONSTRAINT [FK_education_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_gme_rotations] FOREIGN KEY ([refId]) REFERENCES [gme_rotations]([id])
);


GO
CREATE NONCLUSTERED INDEX [ClusteredIndexEducation]
    ON [dbo].[education]([candidate] ASC, [exam_type] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_education_id]
    ON [dbo].[education]([id] ASC);

