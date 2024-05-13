CREATE TABLE [dbo].[AccommodationsId]
(
    [AccommodationId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Code] VARCHAR (100) NOT NULL,
    [DocumentLink]   VARCHAR (1000) ,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL ,
);
