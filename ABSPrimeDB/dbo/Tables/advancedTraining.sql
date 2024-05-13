CREATE TABLE [dbo].[advancedTraining]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [TrainingTypeId] INT NOT NULL, 
    [ProgramId] INT NULL, 
    [Other] VARCHAR(100) NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_advancedTraining_users] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]),
    CONSTRAINT [FK_advancedTraining_trainingtype] FOREIGN KEY ([TrainingTypeId]) REFERENCES [dbo].[trainingType]([Id]),
    CONSTRAINT [FK_advancedTraining_program] FOREIGN KEY ([ProgramId]) REFERENCES [program]([ProgramId]) ,
)
