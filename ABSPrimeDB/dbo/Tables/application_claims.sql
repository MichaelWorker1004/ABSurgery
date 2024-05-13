CREATE TABLE [dbo].[application_claims]
(
    [ApplicationClaimId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [ApplicationId] INT NOT NULL,
    [ClaimName] varchar(50) NOT NULL,
	[CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_applications_applicationid] FOREIGN KEY (ApplicationId) REFERENCES [applications]([ApplicationId])
)
GO
CREATE UNIQUE INDEX [IX_application_claims_claimname] ON [dbo].[application_claims] ([ClaimName], [ApplicationId])
