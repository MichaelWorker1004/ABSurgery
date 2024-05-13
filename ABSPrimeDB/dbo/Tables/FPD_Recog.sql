CREATE TABLE [dbo].[FPD_Recog]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [validityDate] DATETIME NOT NULL,
    [RecognitionIssuanceID]   VARCHAR (10) ,
    [RecognitionIssuanceDate] DATETIME NOT NULL ,
    [RecognitionIssuanceScheduledUpdate] DATETIME NOT NULL,
    [RecognitionIssuanceStatus] VARCHAR (10) NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL ,
    CONSTRAINT [FK_FDP_Certs_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
);