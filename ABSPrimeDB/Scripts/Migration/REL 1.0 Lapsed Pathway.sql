INSERT INTO user_lapsed_pathway (userid, examtype, lapsedpathwaytypeid, startdate, enddate, faildate, note, createdbyuserid, lastupdatedbyuserid)
SELECT
    surgeon_st.userid,
    CASE
        WHEN surgeon_st.status_code in ('LPC','LPCCC') THEN 'C'
        WHEN surgeon_st.status_code in ('LPG','LPCCG') THEN 'G'
        WHEN surgeon_st.status_code in ('LPH','LPCCH') THEN 'H'
        WHEN surgeon_st.status_code in ('LPP','LPCCP') THEN 'P'
        WHEN surgeon_st.status_code in ('LPV','LPCCV') THEN 'V'
    END,
    CASE
        WHEN surgeon_st.status_code in ('LPC','LPG','LPH','LPP','LPV') THEN 1
        WHEN surgeon_st.status_code in ('LPCCC','LPCCG','LPCCH','LPCCP','LPCCV') THEN 2
    END,
    surgeon_st.start_date,
    surgeon_st.end_date,
    SST2.start_date,
    surgeon_st.note+ IIF(SST2.note IS NOT NULL, ' Lasped Fail Note: ' + SST2.note, ''),
    @UserId,
    @UserId
FROM
    dbo.surgeon_st
LEFT JOIN
        surgeon_st AS SST2 ON SST2.userid = surgeon_st.userid and SST2.status_code like 'lf%'
WHERE surgeon_st.status_code like 'lp%'
