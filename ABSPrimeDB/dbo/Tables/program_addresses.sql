CREATE TABLE [dbo].[program_addresses]
(
	[AddressId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [ProgramId] INT NOT NULL,
    [street1] VARCHAR (100) NULL,
    [Street2] VARCHAR (100) NULL,
    [City] VARCHAR (30) NULL,
    [STATE] VARCHAR (2) NULL,
    [ZipCode] CHAR(10) NULL,
    [Country] VARCHAR(50) NULL,
    [AddressType] CHAR(1) NULL,
    [IsMailingAddress] BIT NULL,
	[CreatedByUserId] INT NOT NULL, 
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(), 
    [LastUpdatedByUserId] INT NOT NULL, 
    CONSTRAINT [FK_addresses_programs] FOREIGN KEY ([ProgramId]) REFERENCES [program]([ProgramId]) 
)
GO
CREATE UNIQUE INDEX [IX_addresses_programid_addressid] ON [dbo].[program_addresses] ([AddressId], [ProgramId])
GO