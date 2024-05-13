/*
 Pre-Deployment Script
*/
DECLARE
	@userId INT = 1,
	@versionNumber INT = 0,
	@preDeployment BIT = 1,
	@postDeployment BIT = 0;

/*
Versioning
Follow this pattern to include scripts in the 
pre-deployment and post-deployment scripts

:r .\Versions\0001-post.sql
*/
