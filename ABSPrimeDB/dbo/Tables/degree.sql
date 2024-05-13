CREATE TABLE [dbo].[degree] (
    [degree_id]     INT                   IDENTITY(1,1) PRIMARY KEY,
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [type]          CHAR (1)              NOT NULL,
    [description]   VARCHAR (20)          NULL
);