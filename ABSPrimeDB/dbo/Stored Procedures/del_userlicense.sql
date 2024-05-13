CREATE PROCEDURE [dbo].[del_userlicense]
	@LicenseId INT,
	@UserId INT
AS
	DELETE 
	FROM
		dbo.licenses
	WHERE
		Id = @LicenseId
		AND UserId = @UserId