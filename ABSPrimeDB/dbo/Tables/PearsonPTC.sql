CREATE TABLE [dbo].[PearsonPTC] (
    [City ( Metro Area)] VARCHAR (50)  NULL,
    [PPC Site #]         VARCHAR (10)  NULL,
    [Building Name]      VARCHAR (90)  NULL,
    [Address 1]          VARCHAR (50)  NULL,
    [Address 2]          VARCHAR (50)  NULL,
    [City]               VARCHAR (50)  NULL,
    [State]              VARCHAR (2)   NULL,
    [Zip]                VARCHAR (5)   NULL,
    [Number of Seats]    INT           NULL,
    [Separate Room]      VARCHAR (255) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_PearsonPTC]
    ON [dbo].[PearsonPTC]([Zip] ASC) WITH (FILLFACTOR = 90);

