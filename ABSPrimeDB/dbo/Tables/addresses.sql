CREATE TABLE [dbo].[addresses]
(
	[AddressId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
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
    CONSTRAINT [FK_addresses_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]) 
)
GO
CREATE UNIQUE INDEX [IX_addresses_userid_addressid] ON [dbo].[addresses] ([AddressId], [UserId])
GO