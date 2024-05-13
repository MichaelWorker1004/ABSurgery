CREATE TABLE [dbo].[mcodes] (
    [ID]    NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [Code]  VARCHAR (25)   NULL,
    [Grp]   CHAR (2)       NULL,
    [Descr] VARCHAR (1000) NULL,
    [Attr]  VARCHAR (1000) NULL,
    [Attr2] VARCHAR (8000) NULL,
    [edit]  BIT            CONSTRAINT [DF_mcodes_edit] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_mcodes] PRIMARY KEY NONCLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE CLUSTERED INDEX [index1]
    ON [dbo].[mcodes]([Grp] ASC, [Code] ASC) WITH (FILLFACTOR = 90);

