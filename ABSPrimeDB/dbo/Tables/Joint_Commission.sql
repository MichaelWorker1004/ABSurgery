CREATE TABLE [dbo].[Joint_Commission]
(
    [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [OrganizationId] INT NOT NULL,
    [AccreditationProgram] NVARCHAR(250) NOT NULL,
    [OrganizationName] NVARCHAR(1000) NOT NULL,
    [StreetAddress] NVARCHAR(4000) NOT NULL,
    [City] NVARCHAR(250) NOT NULL,
    [State] VARCHAR(4) NOT NULL,
    [PostalCode] VARCHAR(10) NOT NULL,
    [AccreditationDecision] BIT NOT NULL,
    [AccreditationDecisionDate] DATETIME NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL,
)