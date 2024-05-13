CREATE PROCEDURE [dbo].[get_user_special_accommodations_status_byuserid]
	@UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'Special_Accommodations' Id  
        ,dbo.udf_get_special_accommodations_status(@UserId, @ExamHeaderId) Status
        ,0 disabled