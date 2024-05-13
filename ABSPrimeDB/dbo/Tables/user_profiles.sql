CREATE TABLE [dbo].[user_profiles]
(
	[UserProfileId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [FirstName] VARCHAR(35) NOT NULL DEFAULT '',
    [MiddleName] VARCHAR(35) NOT NULL DEFAULT '',
    [LastName] VARCHAR(35) NOT NULL DEFAULT '',
    [Suffix] varchar(20) NULL,
    [DisplayName] varchar(70) NOT NULL DEFAULT '',
    [OfficePhoneNumber] VARCHAR(15) NULL,
    [MobilePhoneNumber] VARCHAR(15) NULL,
    [BirthCity] VARCHAR(50) NULL,
    [BirthState] VARCHAR(50) NULL,
    [BirthCountry] VARCHAR(50) NULL,
    [CountryCitizenship] VARCHAR(50) NULL,
    [ABSId] varchar(20) NULL,
    [CertificationStatusId] INT NULL,
    [RetirementStatusId] INT NULL DEFAULT 1,
    [NPI] VARCHAR (10) NULL,
    [GenderId] int NULL,
    [BirthDate] Date NULL,
    [Race] varchar(20) NULL,
    [Ethnicity] CHAR(1) NULL,
    [FirstLanguageId] int NULL,
    [BestLanguageId] int NULL,
    [ReceiveComms] bit NULL,
    [QualifiedExamRecord] bit NULL,
    [UserConfirmed] bit NULL,
    [UserConfirmedDate] datetime null,
	[CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_user_profiles_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_user_profiles_certification_status] FOREIGN KEY ([CertificationStatusId]) REFERENCES [certification_status]([CertificationStatusId]),
    CONSTRAINT [FK_user_profiles_retirement_status] FOREIGN KEY ([RetirementStatusId]) REFERENCES [retirement_statuses]([RetirementStatusId])
)
GO
CREATE NONCLUSTERED INDEX [IX_userprofiles_userid] ON [dbo].[user_profiles] ([UserProfileId])
GO
CREATE UNIQUE INDEX [IX_userprofiles_userid] ON [dbo].[users] ([UserId])
GO
CREATE UNIQUE INDEX [IX_user_profiles_absid] ON [dbo].[user_profiles] ([ABSId])
GO
