CREATE TABLE [dbo].[other_entities] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [code]          CHAR (6)              NOT NULL,
    [status]        CHAR (1)              NOT NULL,
    [name]          VARCHAR (60)          NULL,
    [first_name]    VARCHAR (30)          NULL,
    [middle_name]   VARCHAR (30)          NULL,
    [last_name]     VARCHAR (30)          NULL,
    [suffix]        VARCHAR (20)          NULL,
    [degree]        VARCHAR (20)          NULL,
    [title]         VARCHAR (50)          NULL,
    [institution]   VARCHAR (80)          NULL,
    [department]    VARCHAR (50)          NULL,
    [street1]       VARCHAR (100)         NULL,
    [street2]       VARCHAR (100)         NULL,
    [street3]       VARCHAR (50)          NULL,
    [city]          VARCHAR (30)          NULL,
    [state]         CHAR (2)              NULL,
    [zip]           CHAR (10)             NULL,
    [xdate]         SMALLDATETIME         NULL,
    [phone]         VARCHAR (30)          NULL,
    [fax]           VARCHAR (30)          NULL,
    [email]         VARCHAR (100)         NULL,
    [note]          VARCHAR (255)         NULL,
    [mobile]        VARCHAR (30)          NULL,
    [candidate]     AS                    (('E'+right([code],(4)))+[status])
);


GO
CREATE UNIQUE CLUSTERED INDEX [other_entities_uc]
    ON [dbo].[other_entities]([code] ASC, [status] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [other_entities_last_name]
    ON [dbo].[other_entities]([last_name] ASC) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'org1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'other_entities', @level2type = N'COLUMN', @level2name = N'title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'org2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'other_entities', @level2type = N'COLUMN', @level2name = N'institution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'not used', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'other_entities', @level2type = N'COLUMN', @level2name = N'department';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'not used', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'other_entities', @level2type = N'COLUMN', @level2name = N'street3';

