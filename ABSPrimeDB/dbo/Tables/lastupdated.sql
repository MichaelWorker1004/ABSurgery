CREATE TABLE [dbo].[lastupdated] (
    [table_name]  VARCHAR (50) NOT NULL,
    [column_name] VARCHAR (50) NOT NULL,
    [table_id]    INT          NOT NULL,
    [lastupdated] DATETIME     NOT NULL,
    [modifier]    VARCHAR (30) NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20220511-144307]
    ON [dbo].[lastupdated]([table_name] ASC, [column_name] ASC, [table_id] ASC);

