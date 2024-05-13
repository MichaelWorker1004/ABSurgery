CREATE TABLE [dbo].[genders]
(
	[GenderId] INT NOT NULL Identity(1, 1) PRIMARY KEY,
    [Name] varchar(10) NOT NULL,
	[CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL ,
)
