CREATE TABLE [dbo].[gme_rotations]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId]      INT           NOT NULL,
    [StartDate]   DATE          NOT NULL,
    [EndDate]     DATE          NOT NULL,
    [ClinicalLevelId]    INT       NOT NULL,
    [ClinicalActivityId]   INT NOT NULL,
    [ProgramName]    VARCHAR(250) NOT NULL,
    [NonSurgicalActivity]    VARCHAR (500) NULL,
    [AlternateInstitutionName] VARCHAR (500) NULL,
    [IsInternationalRotation]    BIT   NOT NULL,
    [Other]       VARCHAR (500) NULL,
    [FourMonthRotationExplain]    VARCHAR(8000) NULL,
    [NonPrimaryExplain]   VARCHAR(8000) NULL,
    [NonClinicalExplain]  VARCHAR(8000) NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_gme_rotations_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]), 
    CONSTRAINT [FK_gme_rotations_clinicallevel] FOREIGN KEY ([ClinicalLevelId]) REFERENCES [clinical_level]([Id]), 
    CONSTRAINT [FK_gme_rotations_clinicalactivity] FOREIGN KEY ([ClinicalActivityId]) REFERENCES [clinical_activity]([Id])
);
GO
CREATE NONCLUSTERED INDEX [ClusteredIndexEducation]
    ON [dbo].[gme_rotations]([UserId] ASC, [StartDate] ASC) WITH (FILLFACTOR = 90);

GO