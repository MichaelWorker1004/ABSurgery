CREATE TABLE [dbo].[inv_det] (
    [id]       NUMERIC (9)      IDENTITY (1, 1) NOT NULL,
    [created]  [dbo].[created]  NOT NULL,
    [creator]  [dbo].[creator]  NOT NULL,
    [modified] [dbo].[modified] NULL,
    [modifier] [dbo].[modifier] NULL,
    [UserId]   INT              NULL,
    [inv_num]  VARCHAR (16)     NOT NULL,
    [code]     VARCHAR (25)     NOT NULL,
    [amount]   MONEY            NOT NULL,
    [quantity] SMALLINT         NULL,
    [note]     VARCHAR (500)    NULL,
    [trans_id] VARCHAR (46)     NULL,
    CONSTRAINT [PK_inv_det] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_inv_det_users] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_inv_det_inv_num]
    ON [dbo].[inv_det]([inv_num] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_inv_det_code_Includes]
    ON [dbo].[inv_det]([code] ASC)
    INCLUDE([inv_num]);

