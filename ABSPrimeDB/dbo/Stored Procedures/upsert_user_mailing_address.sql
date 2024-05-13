CREATE PROCEDURE [dbo].[upsert_user_mailing_address]
	@UserId int,
	@street1 varchar(100),
	@Street2 varchar(100),
	@City varchar(30),
	@STATE varchar(2),
	@ZipCode char(10),
	@Country varchar(50),
	@CreatedByUserId int,
	@LastUpdatedByUserId int
AS

SET NOCOUNT ON

IF EXISTS(SELECT [UserId] FROM [dbo].[addresses] WHERE [UserId] = @UserId And IsMailingAddress = 1)
BEGIN
	UPDATE [dbo].[addresses] SET
		[UserId] = @UserId,
		[street1] = @street1,
		[Street2] = @Street2,
		[City] = @City,
		[STATE] = @STATE,
		[ZipCode] = @ZipCode,
		[Country] = @Country,
		[LastUpdatedAtUtc] = GETUTCDATE(),
		[LastUpdatedByUserId] = @LastUpdatedByUserId
	WHERE
		[UserId] = @UserId
        And IsMailingAddress = 1
END
ELSE
BEGIN
	INSERT INTO [dbo].[addresses] (
		[UserId],
		[street1],
		[Street2],
		[City],
		[STATE],
		[ZipCode],
		[Country],
		[AddressType],
		[IsMailingAddress],
		[CreatedByUserId],
		[CreatedAtUtc],
		[LastUpdatedAtUtc],
		[LastUpdatedByUserId]
	) VALUES (
		@UserId,
		@street1,
		@Street2,
		@City,
		@STATE,
		@ZipCode,
		@Country,
		'M',
		1,
		@CreatedByUserId,
		GETUTCDATE(),
		GETUTCDATE(),
		@LastUpdatedByUserId
	)
END
