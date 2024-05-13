CREATE TABLE [dbo].[Report_ID] (
    [ID]          NUMERIC (18)  NOT NULL,
    [Name]        VARCHAR (100) NULL,
    [Descr]       VARCHAR (255) NULL,
    [parent_id]   SMALLINT      NULL,
    [Key_ID]      VARCHAR (4)   NULL,
    [jr]          VARCHAR (60)  NULL,
    [con]         VARCHAR (50)  NULL,
    [permissions] TINYINT       CONSTRAINT [DF_Report_ID_permissions] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Report_ID] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);

