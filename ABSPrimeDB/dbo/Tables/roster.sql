CREATE TABLE [dbo].[roster] (
    [exam]         [dbo].[exam]    NOT NULL,
    [created]      [dbo].[created] NOT NULL,
    [id]           NUMERIC (9)     IDENTITY (1, 1) NOT NULL,
    [number]       CHAR (4)        NOT NULL,
    [creator]      [dbo].[creator] NOT NULL,
    [current_year] SMALLINT        NULL,
    [pd_auth]      VARCHAR (100)   NULL,
    [app_rcvd]     SMALLDATETIME   NULL,
    [finalized]    DATETIME        NULL,
    [verified]     DATETIME        NULL,
    [comments]     VARCHAR (1000)  NULL,
    [record_sent]  DATETIME        NULL
);


GO
CREATE CLUSTERED INDEX [CI_CurrentYear]
    ON [dbo].[roster]([number] ASC, [exam] ASC, [current_year] ASC) WITH (FILLFACTOR = 90);

