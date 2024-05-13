CREATE FUNCTION [dbo].[Version_ShouldRunScript] (
		@versionNumber int,
		@isPreDeployment bit
	)
RETURNS BIT
AS
BEGIN
	
	IF EXISTS (
			SELECT
				[VersionId]
			FROM
				[Version]
			WHERE
				[VersionNumber] = @versionNumber
			AND	CASE WHEN @isPreDeployment = 1 THEN [HasPreDeploymentScriptRun]*-1 ELSE [HasPostDeploymentScriptRun]*-1 END = 1
		)
		RETURN 0;

	RETURN 1;

END