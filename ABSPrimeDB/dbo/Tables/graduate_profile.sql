CREATE TABLE [dbo].[graduate_profile]
(
    GraduateProfileId INT NOT NULL PRIMARY KEY,
    [Description]   VARCHAR (100)         NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL ,
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [graduate_profile_uc]
    ON [dbo].[graduate_profile]([Description] ASC) WITH (FILLFACTOR = 90);

