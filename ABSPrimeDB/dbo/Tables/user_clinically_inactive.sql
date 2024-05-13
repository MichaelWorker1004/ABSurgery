CREATE TABLE [dbo].[user_clinically_inactive]
(
    [Id]                                INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId]                            INT NOT NULL,
    [ExamSpecialtyId]                   INT NULL,
    [StartDate]                         DATE NULL,
    [EndDate]                           DATE NULL,
    [Note]                              VARCHAR(2000) NULL,
    [CreatedByUserId]                   INT NOT NULL,
    [CreatedAtUtc]                      DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc]                  DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId]               INT NOT NULL,
    CONSTRAINT [FK_user_clinically_inactive_userid] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]),
    CONSTRAINT [FK_user_clinically_inactive_examspecialtyid] FOREIGN KEY ([ExamSpecialtyId]) REFERENCES [exam_specialties]([Id])
);