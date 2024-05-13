CREATE TABLE [dbo].[country] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [code]          CHAR (3)              NOT NULL,
    [sort_code]     SMALLINT              NOT NULL,
    [description]   VARCHAR (60)          NULL,
    [iso_alpha_2]   CHAR (2)              NULL,
    [iso_alpha_3]   CHAR (3)              NULL,
    [iso_numeric]   CHAR (3)              NULL,
    [iso_code]      CHAR (13)             NULL,
    PRIMARY KEY NONCLUSTERED ([code] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [country_uc]
    ON [dbo].[country]([code] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [country_cd]
    ON [dbo].[country]([code] ASC, [sort_code] ASC) WITH (FILLFACTOR = 90);

