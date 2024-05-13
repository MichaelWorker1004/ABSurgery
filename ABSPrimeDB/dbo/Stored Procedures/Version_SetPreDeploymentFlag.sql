CREATE PROCEDURE [dbo].[Version_SetPreDeploymentFlag]
	@versionNumber int
AS

IF EXISTS ( SELECT [VersionId] FROM [Version] WHERE [VersionNumber] = @versionNumber )
BEGIN
	
	UPDATE
		[Version]
	SET
		[HasPreDeploymentScriptRun] = 1,
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
			@versionNumber, GETUTCDATE(), 1, 0
		);

END;