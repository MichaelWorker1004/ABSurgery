CREATE TABLE [dbo].[user_login_audit]
(
	[UserLoginAuditId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT,
	[EmailAddress] varchar(320) NULL,
	[ApplicationId] INT,
	[LoginDateTimeUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LoginIpAddress] VARCHAR(50) NOT NULL,
	[LoginUserAgent] VARCHAR(500) NOT NULL,
	[LoginSuccess] BIT NOT NULL DEFAULT 0,
	[LoginFailureReason] VARCHAR(500) NULL,
	[CreatedByUserId] INT NOT NULL, 
	[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
	[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
	[LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_user_login_audit_users] FOREIGN KEY ([ApplicationId]) REFERENCES [applications]([ApplicationId])
)
