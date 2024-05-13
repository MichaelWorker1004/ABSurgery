CREATE TABLE [dbo].[exam_exception] (
	[UserId]   	INT				NULL,
	[candidate]	CHAR (6)		NOT NULL,
	[examiner] 	VARCHAR (30) 	NOT NULL,
	[year] 		INT				NULL,
	[area] 		TINYINT 		NULL,
	[reason] 	VARCHAR (100) 	NULL,
	CONSTRAINT [FK_exam_exception_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);

GO
CREATE  CLUSTERED INDEX [exam_exception_examiner] 
	ON [dbo].[exam_exception]([examiner])

GO
CREATE  INDEX [exam_exception_candidate] 
	ON [dbo].[exam_exception]([candidate])

