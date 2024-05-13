CREATE PROCEDURE [dbo].[get_userhospappts_byid]
	@ApptId INT
AS
    SELECT 
        uha.id as ApptId,
        uha.UserId,
        uha.PracticeTypeId,
        pt.Name AS PracticeType,
        uha.AppointmentTypeId,
        uha.PrimaryAppointment,
        apt.Name AS AppointmentType,
        uha.OrganizationTypeId,
        AuthOfficial as AuthorizingOfficial,
        ot.Type AS OrganizationType,
        uha.OrganizationId,
        StateCode,
        Other,
        [Joint_Commission].[OrganizationId] AS OrganizationName
	FROM
		dbo.user_hospital_appointments uha
        LEFT JOIN practice_types pt ON pt.id = uha.PracticeTypeId
        LEFT JOIN appointment_types apt ON apt.id = uha.AppointmentTypeId
        LEFT JOIN organization_type ot ON ot.id = uha.OrganizationTypeId
        LEFT JOIN Joint_Commission ON [Joint_Commission].[OrganizationId] = uha.Organizationid
	WHERE
		uha.Id = @ApptId
