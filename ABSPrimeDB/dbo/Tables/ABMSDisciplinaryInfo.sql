CREATE TABLE [dbo].[ABMSDisciplinaryInfo] (
    [created]                SMALLDATETIME NOT NULL,
    [UserId]                 INT           NULL,
    [candidate]              CHAR (6)      NULL,
    [DisciplinaryEntityName] VARCHAR (100) NULL,
    [Code]                   VARCHAR (10)  NULL,
    [Description]            VARCHAR (100) NULL,
    [OrderDate]              DATE          NULL,
    [ActionCode]             VARCHAR (10)  NULL,
    [ActionDescription]      VARCHAR (100) NULL,
    [ActionCategory]         VARCHAR (100) NULL,
    [Prejudicial]            VARCHAR (3)   NULL,
    CONSTRAINT [FK_abmsdisciplinaryinfo_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);

