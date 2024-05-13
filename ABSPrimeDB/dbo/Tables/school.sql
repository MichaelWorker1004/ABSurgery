CREATE TABLE [dbo].[school] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [type]          CHAR (1)              NOT NULL,
    [description]   VARCHAR (100)         NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [school_uc]
    ON [dbo].[school]([type] ASC) WITH (FILLFACTOR = 90);

