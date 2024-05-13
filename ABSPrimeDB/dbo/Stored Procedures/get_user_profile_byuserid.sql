CREATE PROCEDURE [dbo].[get_user_profile_byuserid]
	@UserId int
AS

SET NOCOUNT ON

SELECT
	[UserProfileId],
	up.[UserId],
	[FirstName],
	[MiddleName],
	[LastName],
	[Suffix],
	[DisplayName],
	[OfficePhoneNumber],
	[MobilePhoneNumber],
	[BirthCity],
	[BirthState],
	[BirthCountry],
	[CountryCitizenship],
	[ABSId],
	cs.Description as CertificationStatus,
	[NPI],
	[GenderId],
	[BirthDate],
	[Race],
	[Ethnicity],
	[FirstLanguageId],
	BestLanguageId,
	[ReceiveComms],
	[UserConfirmed],
	[UserConfirmedDate],
	up.[CreatedByUserId],
	up.[CreatedAtUtc],
	up.[LastUpdatedAtUtc],
	up.[LastUpdatedByUserId],
	a.Street1,
	a.Street2,
	a.City,
	a.State,
	a.ZipCode,
	a.Country
FROM
	[dbo].[user_profiles] up
	left join [dbo].[addresses] a on up.UserId = a.UserId and a.IsMailingAddress = 1
	left join [dbo].[certification_status] cs on up.CertificationStatusId = cs.CertificationStatusId
WHERE
	up.[UserId] = @UserId

GO