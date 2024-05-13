CREATE TABLE [dbo].[Relations] (
    [ID]           NUMERIC (9)   IDENTITY (1, 1) NOT NULL,
    [ParentID]     NUMERIC (9)   NOT NULL,
    [ChildID]      NUMERIC (9)   NOT NULL,
    [Relationship] CHAR (1)      NULL,
    [Date_deleted] SMALLDATETIME NULL
);


GO
CREATE CLUSTERED INDEX [Relations_ParentID]
    ON [dbo].[Relations]([ID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [Relations_ChildID]
    ON [dbo].[Relations]([ChildID] ASC) WITH (FILLFACTOR = 90);

