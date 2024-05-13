CREATE TABLE [dbo].[exam_schedule_links]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ExamScheduleId] INT NOT NULL,
    [ExamHeaderId] INT NOT NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_exam_schedule_links_examschedule] FOREIGN KEY ([ExamScheduleId]) REFERENCES [exam_schedules]([Id]),
    CONSTRAINT [FK_exam_schedule_links_examheader] FOREIGN KEY ([ExamHeaderId]) REFERENCES [Exam_Directory]([Id])
)