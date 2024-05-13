CREATE VIEW 
  DBO.V_EXAMDATES AS
SELECT 
  * 
FROM 
  (SELECT 
    code, 
    grp, 
    CASE WHEN grp='EC' THEN attr ELSE CONVERT(smalldatetime, Descr) END edate 
  FROM 
    mcodes 
  WHERE 
    grp IN ('EC', 
            'EL', 
            'RL') 
  ) 
  AS source pivot (MAX(edate) FOR grp IN ([EC], 
                                          [EL], 
                                          [RL])) AS examdates