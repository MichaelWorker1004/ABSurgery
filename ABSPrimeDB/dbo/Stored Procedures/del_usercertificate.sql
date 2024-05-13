CREATE PROCEDURE [dbo].[del_usercertificate]
	@CertificateId INT,
	@UserId INT
AS
	DELETE 
	FROM
		dbo.user_certificates
	WHERE
		Id = @CertificateId
		AND UserId = @UserId