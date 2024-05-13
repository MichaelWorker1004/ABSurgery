CREATE TABLE [dbo].[exam_cases]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ExamScheduleId] INT NOT NULL,
    [CaseId] INT NOT NULL,
    [SortOrder] INT NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_exam_cases_examscheduleid] FOREIGN KEY ([ExamScheduleId]) REFERENCES [exam_schedules]([Id]),
    CONSTRAINT [FK_exam_cases_caseid] FOREIGN KEY ([CaseId]) REFERENCES [case_headers]([Id])
)