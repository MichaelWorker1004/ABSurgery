CREATE FUNCTION [dbo].[udf_get_meeting_cc_requirements](@UserId INT)
RETURNS INT
AS
BEGIN
-- 0: Not Meeting Requirements, 1: Meeting Requirements, 2: Not Required to Meet CC Requirements
-- To meet requirements:
--      At lesat one active certification
--      Completed an MC application within last 5 years
--      No REVOKED or SUSPENDED disciplinary action
--      no moc_eligibility.optout value above 2005-01-01
-- To be considered not required:
--      Retired status or no moc_eligibility.cohort value
    RETURN (SELECT 
        CASE
            WHEN ISNULL(cmoc_cohort, 0)=0 AND (ep.UserId IS NULL OR t.UserId IS NULL OR da.UserId IS NOT NULL OR ISNULL(me.optout, '1902-01-01')>'2005-01-01') THEN 0 --TL folks rules
            WHEN ISNULL(cmoc_cohort, 0)!=0 AND (ep.UserId IS NULL) THEN 0 --CC folks only need active cert
            -- WHEN up.RetirementStatusId>1 THEN 2
            ELSE 1
        END
    FROM user_profiles up
    LEFT JOIN (select UserId, MAX(isactive) isactive from exam_pass where isactive=1 GROUP BY UserId) ep ON ep.UserId=up.UserId
    LEFT JOIN moc_eligibility me ON me.UserId=up.UserId
    LEFT JOIN (select UserId, MIN(dscpl_code) dscpl_code from dscpl_action da WHERE da.dscpl_code IN ('CR', 'CS') AND da.effective=1 GROUP BY UserId) da ON da.UserId=up.UserId
    LEFT JOIN (select max(year) year,UserId from track where exam='M' and type='C' and app_receive IS NOT NULL group by UserId ) t on t.UserId=up.UserId and t.year >= (select attr from mcodes where grp='AY' and code='M')-5
    -- LEFT JOIN retirement_statuses rs ON rs.RetirementStatusId=up.RetirementStatusId AND rs.Name!='Not Retired'
    WHERE up.UserId=@UserId)
END