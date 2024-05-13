CREATE TABLE [dbo].[exam_teams_planned] (
    [exam]                  [dbo].[exam]    NOT NULL,
    [type]                  CHAR (1)        NOT NULL,
    [year]                   SMALLINT       NOT NULL,
    [area]                  CHAR (1)        NOT NULL,
    [day]                   CHAR (1)        NOT NULL,
    [board_examiner_userid] INT             NULL,
    [board_examiner]        CHAR (6)        NOT NULL,
    [assoc_examiner_userid] INT             NULL,
    [assoc_examiner]        CHAR (6)        NULL,
    [team]                  TINYINT         NOT NULL,
    [grp]                   CHAR (1)        NULL,
    [col]                   TINYINT         NULL,
    [attr]                  TINYINT         CONSTRAINT [DF_exam_teams_planned_attr] DEFAULT (0) NULL,
    [examcode]              AS              (convert(varchar,[year]) + [exam] + [type] + [area]),
    CONSTRAINT [PK_exam_teams_planned] PRIMARY KEY NONCLUSTERED ([exam] ASC, [type] ASC, [year] ASC, [area] ASC, [day] ASC, [team] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_exam_teams_planned_board_users] FOREIGN KEY ([board_examiner_userid]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_exam_teams_planned_assoc_users] FOREIGN KEY ([assoc_examiner_userid]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [exam_teams_planned_col]
    ON [dbo].[exam_teams_planned]([exam] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_teams_cd]
    ON [dbo].[exam_teams_planned]([board_examiner] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_teams_ct]
    ON [dbo].[exam_teams_planned]([assoc_examiner] ASC) WITH (FILLFACTOR = 90);


GO
CREATE UNIQUE NONCLUSTERED INDEX [exam_teams_uc]
    ON [dbo].[exam_teams_planned]([exam] ASC, [type] ASC, [year] ASC, [area] ASC, [day] ASC, [board_examiner] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX ['nc_examcode']
    ON [dbo].[exam_teams_planned]([examcode] ASC) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'decimal coded bitmask; 1 for group leader; 2 for clean team', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'exam_teams_planned', @level2type = N'COLUMN', @level2name = N'attr';

