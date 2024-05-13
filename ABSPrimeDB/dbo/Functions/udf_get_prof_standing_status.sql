CREATE FUNCTION [dbo].[udf_get_prof_standing_status](@UserId INT, @ExamSpecialtyId INT=0)
    RETURNS INT
AS
BEGIN
--  incompelte if:
--      no user_hospital_appt records + no explanation of lack of privileges
    DECLARE @status BIT
        SELECT @status = ISNULL(
            (SELECT
                CASE 
                    WHEN ISNULL(CAST(ups.PrimaryPracticeId AS VARCHAR), ISNULL(ups.ExplanationOfNonPrivileges, ''))='' THEN 0
                    ELSE 1
                END
            FROM user_professional_standing ups
            WHERE ups.UserId=@UserId), 0)
            
    RETURN @status
END
