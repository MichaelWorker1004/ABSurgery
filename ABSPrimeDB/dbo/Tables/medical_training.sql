CREATE TABLE [dbo].[medical_training]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    UserId INT NOT NULL,
    [GraduateProfileId] INT NOT NULL, 
    [MedicalSchoolName] VARCHAR(100) NOT NULL, 
    [MedicalSchoolCity] VARCHAR(50) NOT NULL, 
    [MedicalSchoolStateId] CHAR(2) NULL, 
    [MedicalSchoolCountryId] CHAR(3) NOT NULL, 
    [DegreeId] INT NOT NULL, 
    [MedicalSchoolCompletionYear] CHAR(4) NOT NULL, 
    [ResidencyProgramName] VARCHAR(500) NULL, 
    [ResidencyCompletionYear] CHAR(4) NOT NULL, 
    [ResidencyProgramOther] VARCHAR(8000) NULL, 
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_medical_training_users] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]), 
    CONSTRAINT [FK_medical_training_graduateprofile] FOREIGN KEY ([GraduateProfileId]) REFERENCES [graduate_profile]([GraduateProfileId]), 
    CONSTRAINT [FK_medical_training_degree] FOREIGN KEY ([DegreeId]) REFERENCES [degree]([degree_id]), 
    CONSTRAINT [FK_medical_training_medicalschoolstate] FOREIGN KEY ([MedicalSchoolStateId]) REFERENCES [states_info]([state]), 
    CONSTRAINT [FK_medical_training_country] FOREIGN KEY ([MedicalSchoolCountryId]) REFERENCES [country]([code]) 
)

GO

CREATE UNIQUE INDEX [IX_medical_training_userid] ON [dbo].[medical_training] ([UserId])
