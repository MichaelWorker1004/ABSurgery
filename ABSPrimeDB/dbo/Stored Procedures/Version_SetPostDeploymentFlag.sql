CREATE PROCEDURE [dbo].[Version_SetPostDeploymentFlag]
	@versionNumber int
AS

IF EXISTS ( SELECT [VersionId] FROM [Version] WHERE [VersionNumber] = @versionNumber )
BEGIN
	
	UPDATE
		[Version]
	SET
		[HasPostDeploymentScriptRun] = 1,
		[LastUpdatedAtUtc] = GETUTCDATE()
	WHERE
		[VersionNumber] = @versionNumber;

END
ELSE
BEGIN
	
	INSERT INTO [Version] (
			[VersionNumber], [CreatedAtUtc], [HasPreDeploymentScriptRun], [HasPostDeploymentScriptRun]
		)
	VALUES (
			@versionNumber, GETUTCDATE(), 0, 1
		);

END;