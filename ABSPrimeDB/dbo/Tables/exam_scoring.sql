CREATE TABLE [dbo].[exam_scoring]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ExamCaseId] INT NOT NULL,
    [ExaminerUserId] INT NOT NULL,
    [ExamineeUserId] INT NOT NULL,
    [Score] INT NOT NULL,
    [CriticalFail] BIT NULL,
    [Remarks] VARCHAR(2000) NULL,
    --[Submitted] BIT NULL,
    --[SubmittedDateTimeUTC] datetime NULL,
    [CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_exam_scoring_examcaseid] FOREIGN KEY ([ExamCaseId]) REFERENCES [exam_cases]([Id]),
    CONSTRAINT [FK_exam_scoring_examinerid] FOREIGN KEY ([ExaminerUserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_exam_scoring_examineeid] FOREIGN KEY ([ExamineeUserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [range_exam_scoring_score] CHECK(Score>=0 AND Score<=3),
    CONSTRAINT [unique_exam_scoring_examcaseid_examinerUserid] UNIQUE(ExaminerUserId, ExamCaseId)
)