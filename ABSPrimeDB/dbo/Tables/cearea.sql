CREATE TABLE [dbo].[cearea] (
    [state] CHAR (2) NOT NULL,
    [area]  CHAR (1) NULL,
    CONSTRAINT [IX_cearea] UNIQUE NONCLUSTERED ([state] ASC) WITH (FILLFACTOR = 90)
);

