CREATE TABLE [dbo].[case_ce] (
    [created]    [dbo].[created] NULL,
    [case_num]   VARCHAR (6)     NOT NULL,
    [case_descr] VARCHAR (500)   NULL,
    [pt]         VARCHAR (4)     NULL,
    [id]         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_case_ce]
    ON [dbo].[case_ce]([case_num] ASC) WITH (FILLFACTOR = 90);

