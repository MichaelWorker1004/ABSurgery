CREATE FUNCTION [dbo].[udf_get_pers_info_status](@UserId INT)
RETURNS INT
AS
BEGIN
    DECLARE @status BIT
    SELECT @status =
        IIF(user_profiles.FirstName IS NULL OR
            user_profiles.LastName IS NULL OR
            user_profiles.DisplayName IS NULL OR
            user_profiles.OfficePhoneNumber IS NULL OR
            user_profiles.BirthCountry IS NULL OR
            user_profiles.BirthCity IS NULL OR
            user_profiles.CountryCitizenship IS NULL OR
            user_profiles.GenderId IS NULL OR
            user_profiles.BirthDate IS NULL OR
            user_profiles.Race IS NULL OR
            user_profiles.Ethnicity IS NULL OR
            user_profiles.FirstLanguageId IS NULL OR
            user_profiles.BestLanguageId IS NULL OR
            addresses.street1 IS NULL OR
            addresses.Country IS NULL OR
            addresses.city IS NULL OR
            addresses.ZipCode IS NULL,0,1)
    FROM
        user_profiles
    LEFT JOIN
            dbo.addresses ON addresses.UserId=user_profiles.UserId
    WHERE addresses.UserId=@UserId
RETURN @status
END
