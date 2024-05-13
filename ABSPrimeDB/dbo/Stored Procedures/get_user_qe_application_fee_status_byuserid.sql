CREATE PROCEDURE [dbo].[get_user_qe_application_fee_status_byuserid]
	@UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'Application_Fee' Id  
        ,dbo.udf_get_qe_application_fee_status(@UserId, @ExamHeaderId) Status
        ,0 disabled