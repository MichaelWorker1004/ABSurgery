CREATE PROCEDURE [dbo].[get_cca_application_fee_status_byuserid]
	@UserId INT
AS
    SELECT dbo.udf_get_cc_application_fee_status(@UserId) Status