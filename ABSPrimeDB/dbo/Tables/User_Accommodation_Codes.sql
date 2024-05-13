CREATE TABLE [dbo].[User_Accommodation_Codes] (
    [Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [UserAccommodationId] INT NOT NULL,
    [AccommodationCodeId] INT NOT NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL ,
    CONSTRAINT [FK_User_Accommodation_Codes_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_User_Accommodation_Codes_Ada_Code] FOREIGN KEY ([AccommodationCodeId]) REFERENCES [Accommodation_Codes]([Id]),
    CONSTRAINT [FK_User_Accommodation_Codes_User_Accommodations] FOREIGN KEY ([UserAccommodationId]) REFERENCES [User_Accommodations]([Id])
 );