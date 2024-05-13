CREATE TABLE [dbo].[ABMSdata] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [ABMScode]      CHAR (8)              NULL,
    [ABSID]         INT                   NULL,
    [ABScode]       CHAR (6)              NOT NULL,
    [birthdate]     VARCHAR (10)          NULL,
    [ssn]           [dbo].[ssn]           NULL,
    [first_name]    VARCHAR (45)          NULL,
    [middle_name]   VARCHAR (30)          NULL,
    [last_name]     VARCHAR (45)          NULL,
    [suffix]        VARCHAR (20)          NULL,
    [degreetitle]   VARCHAR (20)          NULL,
    [institution]   VARCHAR (50)          NULL,
    [street1]       VARCHAR (100)         NULL,
    [street2]       VARCHAR (100)         NULL,
    [street3]       VARCHAR (100)         NULL,
    [city]          VARCHAR (30)          NULL,
    [state]         CHAR (2)              NULL,
    [zip]           VARCHAR (10)          NULL,
    [country]       VARCHAR (30)          NULL,
    [email]         VARCHAR (255)         NULL,
    [note]          VARCHAR (255)         NULL,
    PRIMARY KEY NONCLUSTERED ([ABScode] ASC),
    CONSTRAINT [FK_abmsdata_users] FOREIGN KEY ([ABSID]) REFERENCES [Users]([UserId])
);


GO
CREATE UNIQUE CLUSTERED INDEX [ABMSdata_cs]
    ON [dbo].[ABMSdata]([ABScode] ASC) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'not used', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ABMSdata', @level2type = N'COLUMN', @level2name = N'street3';

