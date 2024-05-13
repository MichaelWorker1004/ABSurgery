CREATE TABLE [dbo].[phone_hist] (
    [created]       DATETIME      NULL,
    [creator]       CHAR (3)      NULL,
    [modified]      DATETIME      NULL,
    [modifier]      CHAR (3)      NULL,
    [modifications] SMALLINT      NULL,
    [code]          CHAR (6)      NOT NULL,
    [status]        CHAR (1)      NOT NULL,
    [type1]         CHAR (1)      NOT NULL,
    [type2]         CHAR (1)      NOT NULL,
    [number]        VARCHAR (100) NULL,
    [note]          VARCHAR (255) NULL,
    [id]            NUMERIC (9)   NULL
);

