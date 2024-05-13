CREATE TABLE [dbo].[retirement_statuses]
(
	[RetirementStatusId] INT NOT NULL PRIMARY KEY IDENTITY
	,[Name] VARCHAR(50) NOT NULL
	,[CreatedByUserId] INT NOT NULL
	,[CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate()
	,[LastUpdatedByUserId] INT NOT NULL
)
GO