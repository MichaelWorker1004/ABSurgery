CREATE PROCEDURE [dbo].[get_user_qe_attestation_status_byuserid]
	@UserId INT,
    @ExamHeaderId INT
AS
    SELECT
        'Attestation' Id  
        ,dbo.udf_get_qe_attestation_status(@UserId, @ExamHeaderId) Status
        ,0 disabled