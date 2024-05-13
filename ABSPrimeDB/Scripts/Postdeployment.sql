/*
Post-Deployment Script
*/
DECLARE
	@userId INT = 97755,
	@versionNumber INT = 0,
	@preDeployment BIT = 0,
	@postDeployment BIT = 1;

-- SET THE USERID VARIABLE TO THE USERID OF THE USER THAT IS RUNNING THE SCRIPT

/* 
Follow this pattern to include scripts in the 
pre-deployment and post-deployment scripts
*/
	:r .\Versions\0001-post.sql