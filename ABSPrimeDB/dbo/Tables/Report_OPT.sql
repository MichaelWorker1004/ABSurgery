CREATE TABLE [dbo].[Report_OPT] (
    [ID]         NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Report_ID]  NUMERIC (5)  NULL,
    [Field_Name] VARCHAR (50) NULL,
    [Box_Type]   VARCHAR (7)  NULL,
    [Caption]    VARCHAR (50) NULL,
    [Field_Len]  INT          NULL,
    [Items]      TEXT         NULL,
    [Field_Type] BIT          NULL,
    CONSTRAINT [PK_Report_OPT] PRIMARY KEY NONCLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE CLUSTERED INDEX [IX_Report_OPT]
    ON [dbo].[Report_OPT]([Report_ID] ASC) WITH (FILLFACTOR = 90);

