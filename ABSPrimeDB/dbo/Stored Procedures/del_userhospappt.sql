CREATE PROCEDURE [dbo].[del_userhospappt]
	@ApptId INT,
	@UserId INT
AS
	DELETE 
	FROM
		dbo.user_hospital_appointments
	WHERE
		Id = @ApptId
		AND UserId = @UserId