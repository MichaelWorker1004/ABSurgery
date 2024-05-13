INSERT INTO
    user_clinically_inactive (UserId, ExamSpecialtyId, StartDate, EndDate, Note, CreatedByUserId, LastUpdatedByUserId)
SELECT
    surgeon.UserId,
    exam_specialties.Id,
    surgeon_st.start_date,
    surgeon_st.end_date,
    surgeon_st.note,
    6,
    6
FROM
    dbo.surgeon
LEFT JOIN
    dbo.surgeon_st on surgeon_st.UserId= surgeon.UserId
LEFT JOIN
    V_ABMS_CERT  on V_ABMS_CERT.candidate=surgeon.candidate
LEFT JOIN
    exam_specialties on exam_specialties.code = V_ABMS_CERT.exam
WHERE
    surgeon_st.status_code='CI' AND surgeon_st.UserId IS NOT NULL