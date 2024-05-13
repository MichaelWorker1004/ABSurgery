CREATE TABLE [dbo].[applications] (
    [ApplicationId]   INT          IDENTITY (1, 1) NOT NULL,
    [ApplicationName] VARCHAR (50) NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL 
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([ApplicationId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_Applications_ApplicationName]
    ON [dbo].[Applications]([ApplicationName] ASC);