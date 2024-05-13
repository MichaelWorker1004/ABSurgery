CREATE PROCEDURE [dbo].[update_userhospappt]
    @ApptId INT,
	@UserId INT,
    @PracticeTypeId INT,
    @AppointmentTypeId INT,
    @OrganizationTypeId INT,
    @PrimaryAppointment BIT,
    @StateCode VARCHAR(2), --state code
    @OrganizationId INT, --ID from Joint_Commission
    @AuthorizingOfficial VARCHAR (100),
    @Other VARCHAR (255), --other
	@LastUpdatedByUserId INT
AS
    IF (@PrimaryAppointment = 1) BEGIN
        UPDATE
            user_hospital_appointments
        SET
            PrimaryAppointment = 0
        WHERE
            UserId = @UserId
    END
	UPDATE 
		user_hospital_appointments
    SET
	    UserId              = @UserId,
        PracticeTypeId      = @PracticeTypeId,
        AppointmentTypeId   = @AppointmentTypeId,
        OrganizationTypeId  = @OrganizationTypeId,
        PrimaryAppointment  = @PrimaryAppointment,
        StateCode           = @StateCode,
        OrganizationId      = @OrganizationId,
        AuthOfficial        = @AuthorizingOfficial,
        other               = @Other,
        LastUpdatedAtUTC    = GETUTCDATE(),
        LastUpdatedByUserId = @LastUpdatedByUserId
    WHERE
        Id=@ApptId

	EXEC dbo.get_userhospappts_byid @ApptId