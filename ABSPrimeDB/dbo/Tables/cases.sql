CREATE TABLE [dbo].[cases]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [QuestionID] INT  NULL,
    [OldCaseNumber] VARCHAR(20)  NULL,
    [StemAssets] VARCHAR(100) NULL ,
    [Images] BIT NULL,
    [Title] VARCHAR(MAX) NULL,
    [Testing_Points] VARCHAR(MAX) NULL,
    [Stem] VARCHAR(MAX) NULL,
    [Pertinent_Diagnostics]  VARCHAR(MAX) NULL,
    [Stem_Options] VARCHAR(MAX) NULL,
    [Medical_Preoperative] VARCHAR(MAX) NULL,
    [Operative_Technical_Details] VARCHAR(MAX) NULL,
    [Postoperative_Management_Details] VARCHAR(MAX) NULL,
    [Potential_Options_Contingencies] VARCHAR(MAX) NULL,
    [Potential_Complications] VARCHAR(MAX) NULL,
    [Passing_Points] VARCHAR(MAX) NULL,
    [Errors] VARCHAR(MAX) NULL,
    [Critical_Fails] VARCHAR(MAX) NULL,
    [CreatedByUserId] INT NOT NULL,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId] INT NOT NULL
)
GO
