CREATE PROCEDURE [dbo].[get_user_acgme_status_byuserid]
    @UserId INT
AS
    SELECT
        'ACGME' Id 
        ,dbo.udf_get_acgme_status(@UserId) Status
        ,0 disabled