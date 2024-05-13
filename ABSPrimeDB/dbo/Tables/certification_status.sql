CREATE TABLE [dbo].[certification_status]
(
	[CertificationStatusId] INT NOT NULL Identity(1,1) PRIMARY KEY,
	[Description] varchar(100) not null,
	[StatusGroup] varchar(30) not null,
	[CreatedByUserId] INT NOT NULL,
	[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
	[LastUpdatedByUserId] INT NOT NULL,
	CONSTRAINT [UQ_certification_status_description] UNIQUE([Description])
)
GO