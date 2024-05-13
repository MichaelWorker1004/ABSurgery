CREATE TABLE [dbo].[attestations_text]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [ExamTypeId] INT NOT NULL,
    [ExamSpecialtyId] INT NOT NULL,
    [AttestationText] VARCHAR(MAX) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_attestations_text_exam_type] FOREIGN KEY ([ExamTypeId]) REFERENCES [exam_types]([Id]),
    CONSTRAINT [FK_attestations_text_exam_specialties] FOREIGN KEY ([ExamSpecialtyId]) REFERENCES [exam_specialties]([Id])
)