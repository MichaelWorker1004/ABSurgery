CREATE TABLE [dbo].[user_lapsed_pathway]
(
    [Id]                                INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId]                            INT NOT NULL,
    [ExamType]                          VARCHAR(1) NULL,
    [LapsedPathwayTypeId]               INT NULL,
    [StartDate]                         DATE NULL,
    [EndDate]                           DATE NULL,
    [FailDate]                          DATE NULL,
    [Note]                              VARCHAR(2000) NULL,
    [CreatedByUserId]                   INT NOT NULL,
    [CreatedAtUtc]                      DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc]                  DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId]               INT NOT NULL,
    CONSTRAINT [FK_lapsed_pathway_types_userid] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId]),
    CONSTRAINT [FK_lapsed_pathway_types_LapsedPathwayTypeId] FOREIGN KEY ([LapsedPathwayTypeId]) REFERENCES [lapsed_pathway_types]([Id])
);