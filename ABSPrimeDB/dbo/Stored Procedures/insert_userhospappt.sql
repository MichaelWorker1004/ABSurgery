CREATE PROCEDURE [dbo].[insert_userhospappt]
	@UserId INT,
    @PracticeTypeId INT,
    @AppointmentTypeId INT,
    @OrganizationTypeId INT,
    @StateCode VARCHAR(2), --state code
    @OrganizationId INT, --ID from Joint_Commission
    @PrimaryAppointment BIT,
    @AuthorizingOfficial VARCHAR (100),
    @Other VARCHAR (255), --other
	@CreatedByUserId INT
AS
    IF (@PrimaryAppointment = 1) BEGIN
        UPDATE
            user_hospital_appointments
        SET
            PrimaryAppointment = 0
        WHERE
            UserId = @UserId
    END
	INSERT INTO
		user_hospital_appointments
		(
	        UserId,
            PracticeTypeId,
            AppointmentTypeId,
            OrganizationTypeId,
            PrimaryAppointment,
            StateCode,
            OrganizationId,
            AuthOfficial,
            other,
	        CreatedByUserId,
            LastUpdatedByUserId
		)
		VALUES
		(
	        @UserId,
            @PracticeTypeId,
            @AppointmentTypeId,
            @OrganizationTypeId,
            @PrimaryAppointment,
            @StateCode,
            @OrganizationId,
            @AuthorizingOfficial,
            @other,
	        @CreatedByUserId,
            @UserId
		)
	DECLARE @Id INT
	SET @Id = @@IDENTITY

	EXEC dbo.get_userhospappts_byid @Id
