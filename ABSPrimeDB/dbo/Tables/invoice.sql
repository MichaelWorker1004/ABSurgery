CREATE TABLE [dbo].[invoice] (
    [id]      NUMERIC (9)     IDENTITY (1, 1) NOT NULL,
    [created] [dbo].[created] NOT NULL,
    [creator] [dbo].[creator] NOT NULL,
    [UserId]  INT             NULL,
    [inv_num] VARCHAR (16)    NULL,
    [type]    CHAR (1)        NOT NULL,
    [ref_id]  NUMERIC (9)     NULL,
    [version] TINYINT         NULL,
    [expired] TINYINT         NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_invoice_users] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_invoice_inv_num]
    ON [dbo].[invoice]([inv_num] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_invoice_type_ref_id_Includes]
    ON [dbo].[invoice]([type] ASC, [ref_id] ASC)
    INCLUDE([inv_num]);


GO
CREATE NONCLUSTERED INDEX [IX_invoice_type_ref_id_Includes_inv_num_expired]
    ON [dbo].[invoice]([type] ASC, [ref_id] ASC)
    INCLUDE([inv_num], [expired]);


GO
CREATE NONCLUSTERED INDEX [IX_invoice_type_Includes_inv_num_ref_id]
    ON [dbo].[invoice]([type] ASC)
    INCLUDE([inv_num], [ref_id]);


GO
CREATE NONCLUSTERED INDEX [IX_invoice_type_Includes_inv_num_ref_id_expired]
    ON [dbo].[invoice]([type] ASC)
    INCLUDE([inv_num], [ref_id], [expired]);

