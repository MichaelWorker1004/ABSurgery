CREATE PROCEDURE [dbo].[get_cca_attestation_status_byuserid]
	@UserId INT
AS
    SELECT ISNULL(
        (SELECT 
            CASE 
                WHEN ISNULL(sig_receive, '')=''     AND ISNULL(app_approve, '')=''  THEN 0 --incomplete
                WHEN ISNULL(sig_receive, '')!=''    AND ISNULL(app_approve, '')=''  THEN 1 --pending
                WHEN ISNULL(sig_receive, '')!=''    AND ISNULL(app_approve, '')!='' THEN 2 --complete
            END Status
        FROM track
        WHERE track.UserId=@UserId and examcode=dbo.udf_get_academic_year('M')+'MC'), -1) Status -- -1 = not yet available/contingent
