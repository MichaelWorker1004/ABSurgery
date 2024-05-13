CREATE TABLE [dbo].[fee_det] (
    [ref_id] NUMERIC (9)   NULL,
    [descr]  VARCHAR (80)  NULL,
    [code]   VARCHAR (25)  NULL,
    [note]   VARCHAR (255) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_fee_det_ref_id]
    ON [dbo].[fee_det]([ref_id] ASC) WITH (FILLFACTOR = 90);

