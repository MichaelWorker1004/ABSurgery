CREATE TABLE [dbo].[user_claims]
(
    [UserClaimId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ApplicationClaimId] INT NOT NULL, 
    [UserId] INT NOT NULL,
	[CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_user_claims_users] FOREIGN KEY (UserId) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_user_claims_applicationclaims] FOREIGN KEY ([ApplicationClaimId]) REFERENCES application_claims(ApplicationClaimId) 
)
GO
CREATE UNIQUE INDEX [IX_user_claims_applicationclaimid_userid] ON [dbo].[user_claims] ([ApplicationClaimId], [UserId])
