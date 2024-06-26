CREATE TABLE [outcome_registries]
(
        [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
        [UserId] INT NOT NULL,
        [SurgeonSpecificRegistry] VARCHAR(8000) NULL ,
        [RegistryComments] VARCHAR(8000) NULL ,
        [RegisteredWithACHQC] BIT NOT NULL ,
        [RegisteredWithCESQIP] BIT NOT NULL,
        [RegisteredWithMBSAQIP] BIT NOT NULL,
        [RegisteredWithABA] BIT NOT NULL,
        [RegisteredWithASBS] BIT NOT NULL,
        [RegisteredWithMSQC] BIT NOT NULL,
        [RegisteredWithABMS] BIT NOT NULL,
        [RegisteredWithNCDB] BIT NOT NULL,
        [RegisteredWithRQRS] BIT NOT NULL,
        [RegisteredWithNSQIP] BIT NOT NULL,
        [RegisteredWithNTDB] BIT NOT NULL,
        [RegisteredWithSTS] BIT NOT NULL,
        [RegisteredWithTQIP] BIT NOT NULL,
        [RegisteredWithUNOS] BIT NOT NULL,
        [RegisteredWithNCDR] BIT NOT NULL,
        [RegisteredWithSVS] BIT NOT NULL,
        [RegisteredWithELSO] BIT NOT NULL,
        [RegisteredWithSSR] BIT NOT NULL,
        [UserConfirmed] BIT NULL ,
        [UserConfirmedDateUtc] DATETIME NULL ,
        [CreatedByUserId] INT NOT NULL,
        [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
        [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
        [LastUpdatedByUserId] INT NOT NULL ,
     CONSTRAINT [FK_Outcomes_Registries_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
)