CREATE PROCEDURE [dbo].[get_user_personal_profile_status_byuserid]
    @UserId INT,
    @ExamHeaderId INT
AS
    SELECT 
        'Personal_Profile' Id
        ,dbo.udf_get_personal_profile_status(@UserId, DEFAULT) Status
        ,0 disabled