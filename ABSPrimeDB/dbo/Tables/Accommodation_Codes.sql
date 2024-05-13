CREATE TABLE [dbo].[Accommodation_Codes] (
    [Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Code] VARCHAR (50) NOT NULL,
    [Name] VARCHAR (1000) NOT NULL,
    [Description] VARCHAR (1000) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
 );