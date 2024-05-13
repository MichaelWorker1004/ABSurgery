CREATE TABLE [dbo].[states_info] (
    [created]             [dbo].[created]       NOT NULL,
    [creator]             [dbo].[creator]       NOT NULL,
    [modified]            [dbo].[modified]      NULL,
    [modifier]            [dbo].[modifier]      NULL,
    [modifications]       [dbo].[modifications] NOT NULL,
    [country]             CHAR (3)              NOT NULL,
    [state]               CHAR (2)              NOT NULL,
    [country_description] VARCHAR (40)          NULL,
    [state_description]   VARCHAR (40)          NULL,
    [description]         VARCHAR (255)         NULL,
    PRIMARY KEY NONCLUSTERED ([state] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [states_info_uc]
    ON [dbo].[states_info]([country] ASC, [state] ASC) WITH (FILLFACTOR = 90);

