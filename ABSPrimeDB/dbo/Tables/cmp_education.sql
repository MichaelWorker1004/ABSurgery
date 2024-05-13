CREATE TABLE [dbo].[cmp_education] (
    [UserId]      INT           NULL,
    [candidate]   CHAR (6)      NOT NULL,
    [date_from]   SMALLDATETIME NULL,
    [date_to]     SMALLDATETIME NULL,
    [rot_type]    VARCHAR (50)  NULL,
    [subj_area]   VARCHAR (500) NULL,
    [prg_name]    VARCHAR (500) NULL,
    [other]       VARCHAR (500) NULL,
    [exam_type]   VARCHAR (6)   NULL,
    [institution] VARCHAR (500) NULL,
    [rot_intl]    VARCHAR (3)   NULL,
    CONSTRAINT [FK_cmp_education_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'type of rotation', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'cmp_education', @level2type = N'COLUMN', @level2name = N'rot_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'subject area', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'cmp_education', @level2type = N'COLUMN', @level2name = N'subj_area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'program name', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'cmp_education', @level2type = N'COLUMN', @level2name = N'prg_name';

