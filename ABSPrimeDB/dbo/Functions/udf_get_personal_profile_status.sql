CREATE FUNCTION [dbo].[udf_get_personal_profile_status](@UserId INT, @ExamSpecialtyId INT=0)
RETURNS INT
AS 
BEGIN
    -- incomplete if:
    --      unverified name change request  (TODO)
    --      unverified SSN change request   (TODO)
    --      Required fields not populated:
    --          Display Name
    --          Race
    --          Ethnicity
    --          First language
    --          Second language
    --          Street
    --          City
    --          Phone
    --          Mobile Phone
    --          Email
    --          Birth City
    --          Birth Country
    --          Citizenship
    --          If no NPI, explanation field
    DECLARE @Status INT 
    SELECT @Status=ISNULL(
        (SELECT
            CASE 
                WHEN ISNULL(up.DisplayName, '')=''          THEN 0
                WHEN ISNULL(up.Race, '')=''                 THEN 0
                WHEN ISNULL(up.Ethnicity, '')=''            THEN 0
                WHEN ISNULL(up.FirstLanguageId, '')=''      THEN 0
                WHEN ISNULL(up.BestLanguageId, '')=''       THEN 0
                WHEN ISNULL(a.street1, '')=''               THEN 0
                WHEN ISNULL(a.City, '')=''                  THEN 0
                WHEN ISNULL(up.OfficePhoneNumber, '')=''    THEN 0
                WHEN ISNULL(u.EmailAddress, '')=''          THEN 0
                WHEN ISNULL(up.BirthCity, '')=''            THEN 0
                WHEN ISNULL(up.BirthCountry, '')=''         THEN 0
                WHEN ISNULL(up.CountryCitizenship, '')=''   THEN 0
                WHEN ISNULL(up.NPI, '')=''                  THEN 0
                ELSE 1
            END
        FROM user_profiles up
        LEFT JOIN users u ON u.UserId=up.UserId
        LEFT JOIN addresses a ON a.UserId=up.UserId and IsMailingAddress=1
        WHERE up.UserId=@UserId), 0)

    RETURN @Status
END