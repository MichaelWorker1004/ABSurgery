    CREATE TABLE [dbo].[exam_schedule_scores]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ExamScheduleId] INT NOT NULL,
    [ExaminerUserId] INT NOT NULL,
    [ExaminerScore] INT NOT NULL,
    [Submitted] BIT NULL,
    [SubmittedDateTimeUTC] datetime NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL,
    CONSTRAINT [FK_exam_schedule_scores_examinerid] FOREIGN KEY ([ExaminerUserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_exam_schedule_scores_examschedule] FOREIGN KEY ([ExamScheduleId]) REFERENCES [exam_schedules]([Id])
)