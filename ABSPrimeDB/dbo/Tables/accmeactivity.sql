CREATE TABLE [dbo].[accmeactivity] (
    [id]                    NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [created]               DATETIME       NULL,
    [modified]              DATETIME       NULL,
    [modifications]         INT            CONSTRAINT [DF_accmeactivity_modifications] DEFAULT ((0)) NULL,
    [activityid]            NUMERIC (10)   NULL,
    [provideractivityid]    VARCHAR (256)  NULL,
    [ReportingStartDate]    DATETIME       NULL,
    [ReportingEndDate]      DATETIME       NULL,
    [URL]                   VARCHAR (2083) NULL,
    [title]                 VARCHAR (1000) NULL,
    [accreditedprovider]    VARCHAR (1000) NULL,
    [nonaccreditedprovider] VARCHAR (1000) NULL,
    [category1_credits]     NUMERIC (5, 2) NULL,
    [cc_credits]            NUMERIC (5, 2) NULL,
    [cc_sam_credits]        NUMERIC (5, 2) NULL,
    [startDateTime]         DATETIME       NULL,
    [endDateTime]           DATETIME       NULL,
    [activitySponsorship]   VARCHAR (256)   NULL,
    [activityFormat]        VARCHAR (256)   NULL,
    [lomsource]             VARCHAR (50)   NULL,
    [lomvalue]              VARCHAR (50)   NULL,
    [lomentity]             VARCHAR (50)   NULL,
    [lomduration]           VARCHAR (50)   NULL,
    [variableMOC]           VARCHAR (5)    NULL,
    [patientsafetyApproval] VARCHAR (5)    NULL,
    [recordStatus]          VARCHAR (50)   NULL,
    [feeForParticipation]   VARCHAR (5)    NULL,
    [activityRegistration]  VARCHAR (50)   NULL
);


GO
CREATE CLUSTERED INDEX [ClusteredIndex-20200609-125717]
    ON [dbo].[accmeactivity]([activityid] ASC) WITH (FILLFACTOR = 90);

