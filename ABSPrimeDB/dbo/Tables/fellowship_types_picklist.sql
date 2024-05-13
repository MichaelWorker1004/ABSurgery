CREATE TABLE[fellowship_types_picklist]
(
    [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [FellowshipType] VARCHAR(1) NULL,
    [FellowshipTypeName] VARCHAR(50) NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [CreatedByUserId] INT NOT NULL,
    [LastUpdatedByUserId] INT NOT NULL
)