UPDATE user_profiles 
SET 
    CertificationStatusId=CASE
        WHEN ISNULL(isactive, 0)<1 THEN nc.CertificationStatusId
        WHEN ISNULL(ep.isactive, 0)>=1 and da.dscpl_code IS NULL THEN c.CertificationStatusId
        WHEN da.dscpl_code='CR' THEN ncr.CertificationStatusId
        WHEN da.dscpl_code='CS' THEN ncs.CertificationStatusId
        ELSE nc.CertificationStatusId
    END
FROM user_profiles up
LEFT JOIN dscpl_action da ON da.UserId=up.UserId AND da.dscpl_code IN ('CR', 'CS') AND da.effective=1
LEFT JOIN (select UserId, MAX(isactive) isactive from exam_pass where isactive=1 GROUP BY UserId) ep ON ep.UserId=up.UserId
LEFT JOIN certification_status c ON c.[Description]='Certified'
LEFT JOIN certification_status ncr ON ncr.[Description]='Not Certified - Revoked'
LEFT JOIN certification_status ncs ON ncs.[Description]='Not Certified - Suspended'
LEFT JOIN certification_status nc ON nc.[Description]='Not Certified'
LEFT JOIN certification_status clp ON clp.[Description]='Certified in Lapsed Pathway for at least one specialty'