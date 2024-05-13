CREATE TABLE [dbo].[race] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [type]          CHAR (1)              NOT NULL,
    [description]   VARCHAR (20)          NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [race_uc]
    ON [dbo].[race]([type] ASC) WITH (FILLFACTOR = 90);

