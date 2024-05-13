CREATE TABLE [dbo].[Version]
(
	[VersionId] INT IDENTITY NOT NULL,
	[VersionNumber] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [HasPreDeploymentScriptRun] BIT NOT NULL DEFAULT 0, 
    [HasPostDeploymentScriptRun] BIT NOT NULL DEFAULT 0 
)