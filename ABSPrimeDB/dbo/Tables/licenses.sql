CREATE TABLE [dbo].[licenses] (
    [id]                    NUMERIC (10)  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ABMSUID]               VARCHAR (8)   NULL,
    [UserId]                INT           NULL,
    [candidate]             CHAR (6)      NULL,
    [Last4SSN]              CHAR (4)      NULL,
    [FirstName]             VARCHAR (30)  NULL,
    [MiddleName]            VARCHAR (30)  NULL,
    [LastName]              VARCHAR (30)  NULL,
    [Suffix]                VARCHAR (20)  NULL,
    [Gender]                CHAR (1)      NULL,
    [PhysicianStatus]       VARCHAR (8)   NULL,
    [DeceasedDate]          DATE          NULL,
    [BirthDate]             DATE          NULL,
    [Email]                 VARCHAR (100) NULL,
    [LicensingStateCode]    VARCHAR (2)   NULL,
    [LicenseNumber]         VARCHAR (20)  NULL,
    [LicenseIssueDate]      DATE          NULL,
    [LicenseExpireDate]     DATE          NULL,
    [LicenseTypeCode]       VARCHAR (5)   NULL,
    [LicenseTypeId]         INT           NULL,
    [VerifyingOrganization] VARCHAR (50)  CONSTRAINT [DF_FSMB_ReportingOrganization] DEFAULT ('SELF') NULL,
    [NPI]                   VARCHAR (10)  NULL,
    [validityDate]          DATE          NULL,
    CONSTRAINT [PK_FSMB] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_licenses_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
);


GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170217-143542]
    ON [dbo].[licenses]([candidate] ASC) WITH (FILLFACTOR = 90);

