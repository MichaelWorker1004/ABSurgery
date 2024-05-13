CREATE PROCEDURE [dbo].[get_user_certification_current_status_byuserid]
    @UserId int
AS
    -- 1 - Certified
    --      has at least one unexpired exam_pass entry
    --      no active entry in user_lapsed_pathway
    --      no active SUSPENDED or REVOKED entry in dscpl_action
    -- 2 - Not Certified - Revoked
    --      license REVOKED in dscpl_action
    -- 3 - Not Certified - Suspended
    --      license SUSPENDED in dscpl_action
    -- 5 - Trainee
    --      Trainee type role without Surgeon type role in user_claims
    -- 8 - Certified in lapsed pathway
    --      At least one unexpired exam_pass entry
    --      At lesat one active entry in user_lapsed_pathway
    --      no active SUSPENDED or REVOKED entry in dscpl_action
    -- 9 - Not Certified
    --      No active exam_pass entry
    --      no disciplinary actions either
    SELECT
        CASE
            WHEN da.dscpl_code='CR' THEN ncr.CertificationStatusId
            WHEN da.dscpl_code='CS' THEN ncs.CertificationStatusId
            WHEN uct.UserId IS NOT NULL and ucs.UserId IS NULL THEN t.CertificationStatusId
            WHEN ISNULL(isactive, 0)<1 THEN nc.CertificationStatusId
            WHEN ISNULL(ep.isactive, 0)>=1 and da.dscpl_code IS NULL AND ISNULL(ulp.EndDate, '1902-01-01')>GETDATE() THEN clp.CertificationStatusId
            WHEN ISNULL(ep.isactive, 0)>=1 and da.dscpl_code IS NULL AND ISNULL(ulp.EndDate, '')='' THEN c.CertificationStatusId
            ELSE nc.CertificationStatusId
        END CertificationStatusId
        ,CASE
            WHEN da.dscpl_code='CR' THEN ncr.Description
            WHEN da.dscpl_code='CS' THEN ncs.Description
            WHEN uct.UserId IS NOT NULL and ucs.UserId IS NULL THEN t.Description
            WHEN ISNULL(isactive, 0)<1 THEN nc.Description
            WHEN ISNULL(ep.isactive, 0)>=1 and da.dscpl_code IS NULL AND ISNULL(ulp.EndDate, '1902-01-01')>GETDATE() THEN clp.Description
            WHEN ISNULL(ep.isactive, 0)>=1 and da.dscpl_code IS NULL THEN c.Description
        ELSE nc.Description
    END Description
    FROM user_profiles up
    LEFT JOIN (select UserId, MIN(dscpl_code) dscpl_code from dscpl_action da WHERE da.dscpl_code IN ('CR', 'CS') AND da.effective=1 GROUP BY UserId) da ON da.UserId=up.UserId
    LEFT JOIN (select UserId, MAX(isactive) isactive from exam_pass where isactive=1 GROUP BY UserId) ep ON ep.UserId=up.UserId
    LEFT JOIN application_claims act ON act.ClaimName='Trainee'
    LEFT JOIN user_claims uct ON uct.UserId=up.UserId AND uct.ApplicationClaimId=act.ApplicationId
    LEFT JOIN application_claims acs ON acs.ClaimName='Surgeon'
    LEFT JOIN user_claims ucs ON ucs.UserId=up.UserId AND ucs.ApplicationClaimId=acs.ApplicationId 
    LEFT JOIN (select UserId, MAX(EndDate) EndDate FROM user_lapsed_pathway WHERE user_lapsed_pathway.FailDate IS NULL AND user_lapsed_pathway.EndDate>GETDATE() GROUP BY UserId) ulp ON ulp.UserId=up.UserId
    LEFT JOIN certification_status c ON c.[Description]='Certified'
    LEFT JOIN certification_status ncr ON ncr.[Description]='Not Certified - Revoked'
    LEFT JOIN certification_status ncs ON ncs.[Description]='Not Certified - Suspended'
    LEFT JOIN certification_status t ON t.[Description]='Trainee'
    LEFT JOIN certification_status clp ON clp.[Description]='Certified in Lapsed Pathway for at least one specialty' 
    LEFT JOIN certification_status nc ON nc.[Description]='Not Certified'
    WHERE up.UserId=@UserId
