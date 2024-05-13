CREATE TABLE [dbo].[user_professional_standing]
(
    [Id]                                INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId]                            INT NOT NULL,
    [PrimaryPracticeId]                 INT NULL ,
    [OrganizationTypeId]                INT NULL ,
    [ExplanationOfNonPrivileges]        VARCHAR(MAX) NULL,
    [ExplanationOfNonClinicalActivities]VARCHAR(MAX) NULL,
    [CreatedByUserId]                   INT NOT NULL,
    [CreatedAtUtc]                      DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc]                  DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId]               INT NOT NULL ,
    CONSTRAINT [FK_User_professional_standing] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_User_Pracrice] FOREIGN KEY ([PrimaryPracticeId]) REFERENCES [Primary_Practice]([Id]),
    CONSTRAINT [FK_User_Org_type] FOREIGN KEY ([OrganizationTypeId]) REFERENCES [Organization_Type]([Id]),
);