CREATE PROCEDURE [dbo].[update_outcome_registries]
    @UserId INT,
    @SurgeonSpecificRegistry VARCHAR(8000),
    @RegistryComments VARCHAR(8000),
    @RegisteredWithACHQC BIT,
    @RegisteredWithCESQIP BIT,
    @RegisteredWithMBSAQIP BIT,
    @RegisteredWithABA BIT,
    @RegisteredWithASBS BIT,
    @RegisteredWithMSQC BIT,
    @RegisteredWithABMS BIT,
    @RegisteredWithNCDB BIT,
    @RegisteredWithRQRS BIT,
    @RegisteredWithNSQIP BIT,
    @RegisteredWithNTDB BIT,
    @RegisteredWithSTS  BIT,
    @RegisteredWithTQIP BIT,
    @RegisteredWithUNOS BIT,
    @RegisteredWithNCDR BIT,
    @RegisteredWithSVS BIT,
    @RegisteredWithELSO BIT,
    @RegisteredWithSSR BIT,
    @UserConfirmed BIT,
    @UserConfirmedDateUtc DATETIME,
    @LastUpdatedByUserId INT
AS
	SET NOCOUNT ON

	Update [dbo].[outcome_registries] SET
		SurgeonSpecificRegistry = @SurgeonSpecificRegistry,
		RegistryComments = @RegistryComments,
		RegisteredWithACHQC = @RegisteredWithACHQC,
		RegisteredWithCESQIP = @RegisteredWithCESQIP,
		RegisteredWithMBSAQIP = @RegisteredWithMBSAQIP,
		RegisteredWithABA = @RegisteredWithABA,
		RegisteredWithASBS = @RegisteredWithASBS,
		RegisteredWithMSQC = @RegisteredWithMSQC,
		RegisteredWithABMS = @RegisteredWithABMS,
		RegisteredWithNCDB = @RegisteredWithNCDB,
		RegisteredWithRQRS = @RegisteredWithRQRS,
		RegisteredWithNSQIP = @RegisteredWithNSQIP,
		RegisteredWithNTDB = @RegisteredWithNTDB,
		RegisteredWithSTS = @RegisteredWithSTS,
		RegisteredWithTQIP = @RegisteredWithTQIP,
		RegisteredWithUNOS = @RegisteredWithUNOS,
		RegisteredWithNCDR = @RegisteredWithNCDR,
		RegisteredWithSVS = @RegisteredWithSVS,
		RegisteredWithELSO = @RegisteredWithELSO,
		RegisteredWithSSR = @RegisteredWithSSR,
		UserConfirmed = @UserConfirmed,
		UserConfirmedDateUtc = @UserConfirmedDateUtc,	
		LastUpdatedByUserId = @LastUpdatedByUserId,
		LastUpdatedAtUtc = GetUtcDate()
	WHERE
		UserId= @UserId

EXEC get_outcome_registries @UserId