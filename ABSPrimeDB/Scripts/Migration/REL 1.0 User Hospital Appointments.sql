INSERT INTO 
    user_hospital_appointments
(
    UserId,
    PracticeTypeId,
    AppointmentTypeId,
    OrganizationTypeId,
    StateCode,
    OrganizationId,
    AuthOfficial,
    Other,
    hosp_num,
    CreatedAtUTC,
    LastUpdatedAtUtc,
    CreatedByUserId,
    LastUpdatedByUserId
)
SELECT 
    UserId,
    pt.id PracticeTypeId,
    apt.id AppointmentTypeId,
    ot.id OrganizationTypeId,
    q28 StateCode,
    Joint_Commission.OrganizationId OrganizationId,
    q36 AuthOfficial,
    q32 Other,
    hosp_num,
    ISNULL(created, GETUTCDATE()) CreatedAtUTC,
    GETUTCDATE() LastUpdatedAtUtc,
    @UserId CreatedByUserId,
    @UserId LastUpdatedByUserId
FROM AppReplyCardsPractice
LEFT JOIN practice_types pt ON pt.Name=q215
LEFT JOIN appointment_types apt ON apt.Name=q237
LEFT JOIN organization_type ot ON ot.Type=q105
LEFT JOIN Joint_Commission ON Joint_Commission.OrganizationId=SUBSTRING(q212, NULLIF(CHARINDEX('[', q212), 0)+1, NULLIF(CHARINDEX(']', q212), 0)-NULLIF(CHARINDEX('[', q212), 0)-1)