CREATE TABLE [dbo].[user_hospital_appointments] (
    [Id]                    NUMERIC (9)     IDENTITY (1, 1) NOT NULL,
    [UserId]                INT             NULL,
    [PracticeTypeId]        INT             NULL,
    [AppointmentTypeId]     INT             NULL,
    [OrganizationTypeId]    INT             NULL,
    [PrimaryAppointment]    BIT             NULL,
    [StateCode]             VARCHAR (2)     NULL, --state code
    [OrganizationId]        INT             NULL, --ID from jcaho
    [AuthOfficial]          VARCHAR (100)   NULL, --authenticating official
    [other]                 VARCHAR (255)   NULL, --other
    [hosp_num]              TINYINT         NULL,
    [CreatedByUserId]       INT             NOT NULL, 
    [CreatedAtUtc]          DATETIME        NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc]      DATETIME        NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId]   INT             NOT NULL, 
    CONSTRAINT [FK_user_hospital_appointments_userid] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_user_hospital_appointments_practicetypeid] FOREIGN KEY ([PracticeTypeId]) REFERENCES [practice_types]([Id]),
    CONSTRAINT [FK_user_hospital_appointments_appointmenttypeid] FOREIGN KEY ([AppointmentTypeId]) REFERENCES [appointment_types]([Id]),
    CONSTRAINT [FK_user_hospital_appointments_organizationtypeid] FOREIGN KEY ([OrganizationTypeId]) REFERENCES [organization_type]([Id])
);

