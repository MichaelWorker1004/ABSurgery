CREATE TABLE [dbo].[users]
(
	[UserId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [EmailAddress] VARCHAR(320)  NULL,
    [UserName] VARCHAR(50) NULL,
    [Password] VARBINARY(64) NOT NULL ,
    [SALT] varchar(36) NOT NULL DEFAULT CONVERT(VARCHAR(36), NEWID()),
    [PasswordLastModified] DATE NULL,
    [ResetRequired] BIT NOT NULL DEFAULT 0,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL ,
    --CONSTRAINT [UQ_username] UNIQUE([UserName])
)
GO