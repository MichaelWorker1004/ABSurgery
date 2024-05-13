CREATE TABLE [dbo].[reset_guids](
    UserId [int] NOT NULL PRIMARY KEY,
    ResetGUID [uniqueidentifier] NOT NULL,
    ExpireDate [datetime]  NULL,
    CONSTRAINT [FK_Reset_Guids_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
)
