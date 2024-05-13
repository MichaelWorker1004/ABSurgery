CREATE TABLE [dbo].[User_Accommodations]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [AccommodationID] INT NOT NULL,
    [DocumentId] INT,
    [ExamID]   INT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL ,
    CONSTRAINT [FK_User_Accommodations_ExamId] FOREIGN KEY ([examID]) REFERENCES [Exam_Directory]([Id]),
    CONSTRAINT [FK_User_Accommodations_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [User_Documents]([Id]),
    CONSTRAINT [FK_User_Accommodations_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_User_Accommodations_AdaCode] FOREIGN KEY ([AccommodationID]) REFERENCES [AccommodationsId]([AccommodationID])
);