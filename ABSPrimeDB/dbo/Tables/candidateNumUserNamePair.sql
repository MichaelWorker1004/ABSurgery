CREATE TABLE [dbo].[candidateNumUserNamePair]
(
    [TempUName] VARCHAR(100) NOT NULL,
    [UserName]  VARCHAR(100) NOT NULL UNIQUE,
    [Candidate] CHAR(10)     NOT NULL UNIQUE
)