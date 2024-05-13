CREATE TABLE [dbo].[ABMSCertificateInfo] (
    [created]                        SMALLDATETIME NOT NULL,
    [UserId]                         INT           NULL,
    [candidate]                      CHAR (6)      NULL,
    [CertificationBoard]             VARCHAR (10)  NULL,
    [CertificateName]                VARCHAR (10)  NULL,
    [certificateID]                  VARCHAR (5)   NULL,
    [CertificateType]                VARCHAR (20)  NULL,
    [CertificateMaintenance]         VARCHAR (15)  NULL,
    [CertificateMaintenanceRequired] VARCHAR (15)  NULL,
    [IssuanceID]                     VARCHAR (50)  NULL,
    [IssuanceFocus]                  VARCHAR (50)  NULL,
    [RecognitionName]                VARCHAR (100) NULL,
    [RecognitionType]                VARCHAR (100) NULL,
    [IssuanceDuration]               VARCHAR (15)  NULL,
    [IssuanceOccurrence]             VARCHAR (15)  NULL,
    [IssueDate]                      VARCHAR (10)  NULL,
    [ExpireDate]                     VARCHAR (10)  NULL,
    [IssuanceStatus]                 VARCHAR (10)  NULL,
    CONSTRAINT [FK_abmscertificateinfo_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);

