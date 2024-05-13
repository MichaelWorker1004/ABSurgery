CREATE TABLE [dbo].[user_sanctions]
(
    [Id]                                INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId]                            INT NOT NULL,
    [HadDrugAlchoholTreatment]          BIT NOT NULL CONSTRAINT [DF_user_sanctions_Substance_abuse]  DEFAULT (0),
    [HadHospitalPrivilegesDenied]       BIT NOT NULL CONSTRAINT [DF_user_sanctions_Denied_Hospital_Privileges]  DEFAULT (0),
    [HadLicenseRestricted]              BIT NOT NULL CONSTRAINT [DF_user_sanctions_License_Actions]  DEFAULT (0),
    [HadHospitalPrivilegesRestricted]   BIT NOT NULL CONSTRAINT [DF_user_sanctions_Denied_Staff_Privileges]  DEFAULT (0),
    [HadFelonyConviction]               BIT NOT NULL CONSTRAINT [DF_user_sanctions_Felon]  DEFAULT (0),
    [HadCensure]                        BIT NOT NULL CONSTRAINT [DF_user_sanctions_Censured]  DEFAULT (0),
    [Explanation]                       VARCHAR(MAX) NULL,
    [Verified]                          DATETIME NULL,
    [VerifiedByUserId]                  INT NULL,
    [CreatedByUserId]                   INT NOT NULL,
    [CreatedAtUtc]                      DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc]                  DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId]               INT NOT NULL ,
    CONSTRAINT [FK_user_sanctions] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
);