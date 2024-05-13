CREATE PROCEDURE [dbo].[get_user_meeting_cc_requirements_byuserid]
	@UserId INT
AS
    DECLARE @cc_status INT=dbo.udf_get_meeting_cc_requirements(@UserId)

    SELECT
        CASE
            WHEN @cc_status=0 THEN 'No'
            WHEN @cc_status=1 THEN 'Yes'
            WHEN @cc_status=2 THEN 'Not Required'
        END MeetingRequirements