CREATE TABLE [dbo].[training_type] (
    [Id]    INT IDENTITY(1,1) PRIMARY KEY,
    [TrainingType]  VARCHAR (100) NOT NULL
);
GO

CREATE UNIQUE INDEX [IX_trainingType_trainingtype] ON [dbo].[training_type] ([TrainingType])
