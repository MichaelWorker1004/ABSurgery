CREATE PROCEDURE [dbo].[update_user_profile]
@UserProfileId int,
	@UserId int,
	@FirstName varchar(35),
	@MiddleName varchar(35),
	@Suffix varchar(5),
	@DisplayName varchar(70),
	@OfficePhoneNumber varchar(15),
	@MobilePhoneNumber varchar(15),
	@BirthCity varchar(50),
	@BirthState varchar(50),
	@BirthCountry varchar(50),
	@CountryCitizenship varchar(50),
	@ABSId varchar(20),
	@NPI varchar(10),
	@GenderId int = NULL,
	@BirthDate date,
	@Race varchar(20),
	@Ethnicity char(1),
	@FirstLanguageId int,
	@BestLanguageId int,
	@ReceiveComms bit,
	@UserConfirmed bit,
	@UserConfirmedDate datetime,
    @Street1 varchar(100),
    @Street2 VARCHAR(100),
    @City varchar(30),
    @State varchar(2),
    @ZipCode char(10),
    @Country varchar(50),
	@CreatedByUserId int,
	@CreatedAtUtc datetime,
	@LastUpdatedAtUtc datetime,
	@LastUpdatedByUserId int
AS

SET NOCOUNT ON

UPDATE [dbo].[user_profiles] SET
	[UserId] = @UserId,
	[FirstName] = @FirstName,
	[MiddleName] = @MiddleName,
	[Suffix] = @Suffix,
	[DisplayName] = @DisplayName,
	[OfficePhoneNumber] = @OfficePhoneNumber,
	[MobilePhoneNumber] = @MobilePhoneNumber,
	[BirthCity] = @BirthCity,
	[BirthState] = @BirthState,
	[BirthCountry] = @BirthCountry,
	[CountryCitizenship] = @CountryCitizenship,
	[ABSId] = @ABSId,
	[NPI] = @NPI,
	[GenderId] = @GenderId,
	[BirthDate] = @BirthDate,
	[Race] = @Race,
	[Ethnicity] = @Ethnicity,
	[FirstLanguageId] = @FirstLanguageId,
	[BestLanguageId] = @BestLanguageId,
	[ReceiveComms] = @ReceiveComms,
	[UserConfirmed] = @UserConfirmed,
	[UserConfirmedDate] = @UserConfirmedDate,
	[CreatedByUserId] = @CreatedByUserId,
	[CreatedAtUtc] = @CreatedAtUtc,
	[LastUpdatedAtUtc] = @LastUpdatedAtUtc,
	[LastUpdatedByUserId] = @LastUpdatedByUserId
WHERE
	[UserProfileId] = @UserProfileId

EXEC [dbo].[upsert_user_mailing_address] @UserId, @Street1, @Street2, @City, @State, @ZipCode, @Country, @CreatedByUserId, @LastUpdatedByUserId

EXEC [dbo].[get_user_profile_byuserid] @UserId

GO