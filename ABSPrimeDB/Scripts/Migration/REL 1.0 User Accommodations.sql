INSERT INTO User_Accommodations(UserID,AccommodationID,examID,createdbyuserid, lastupdatedbyuserid)
SELECT
    user_profiles.UserID,
    AccommodationsId.AccommodationId,
    Exam_Directory.Id,
    @UserId,
    @UserId
FROM
    user_profiles
LEFT JOIN
    surgeon_st on user_profiles.UserId=surgeon_st.UserId
LEFT JOIN
    Exam_Directory on surgeon_st.st_val=Exam_Directory.ExamCode
LEFT JOIN
    AccommodationsId on surgeon_st.status_code=AccommodationsId.Code
WHERE
    AccommodationId IS NOT NULL