CREATE VIEW V_EXAM_TRACK AS
SELECT                                   *                                ,
       dbo.udf_get_fee_received_date(cand+trackcode+'A') AS appfee_rcvd   ,
       dbo.udf_get_balance(cand          +trackcode+'A') AS appfee_balance,
       dbo.udf_get_fee_received_date(cand+examcode+'E')  AS regfee_rcvd   ,
       dbo.udf_get_balance(cand          +examcode+'E')  AS regfee_balance
FROM   ( SELECT exam.candidate       AS                           cand     ,
               exam.UserId                                                 ,
               RTRIM(track.examcode) AS                           trackcode,
               RTRIM(exam.examcode)  AS                           examcode ,
               exam.exam                                                   ,
               exam.type_dec AS type                                       ,
               exam.type     AS examtype                                   ,
               exam.year                                                   ,
               ISNULL(RTRIM(area),'') area                                 ,
               DAY                                                         ,
               briefing                                                    ,
               app_receive  AS app_rcvd                                     ,
               app_approve  AS app_appr                                     ,
               oper_receive AS oper_rcvd                                    ,
               oper_approve AS oper_appr                                    ,
               rply_sent                                                    ,
               rply_rcvd                                                    ,
               pref_rcvd                                                    ,
               adms_sent                                                    ,
               pearson_sent                                                 ,
               sig_receive         AS sig_rcvd                                      ,
               center              AS cntr                                          ,
               result              AS pf                                            ,
               ISNULL(status,'')   AS status                                        ,
               ISNULL(exam.id, 0)  AS examid                                        ,
               ISNULL(track.id, 0) AS trackid
       FROM    v_exam_dec exam
               LEFT OUTER JOIN track
               ON      track.UserId=exam.UserId
               AND     track.exam     =exam.exam
               AND     track.type     =exam.type_dec
               AND     track.year     =exam.year
       
       UNION
       
       SELECT track.candidate       AS cand     ,
              track.UserId                      ,
              RTRIM(track.examcode) AS trackcode,
              RTRIM(exam.examcode)  AS examcode ,
              track.exam                        ,
              track.type                        ,
              NULL                              ,
              track.year                        ,
              ISNULL(RTRIM(area),'')    area       ,
              ''                     AS DAY        ,
              ''                     AS briefing   ,
              app_receive            AS app_rcvd   ,
              app_approve            AS app_appr   ,
              oper_receive           AS oper_rcvd  ,
              oper_approve           AS oper_appr  ,
              rply_sent                            ,
              rply_rcvd                            ,
              pref_rcvd                            ,
              adms_sent                            ,
              pearson_sent                         ,
              sig_receive         AS sig_rcvd              ,
              center              AS cntr                  ,
              result              AS pf                    ,
              ISNULL(status,'')   AS status                ,
              ISNULL(exam.id, 0)  AS examid                ,
              ISNULL(track.id, 0) AS trackid
       FROM   track
              LEFT OUTER JOIN v_exam_dec exam
              ON     track.UserId=exam.UserId
              AND    track.exam     =exam.exam
              AND    track.type     =exam.type_dec
              AND    track.year     =exam.year
       WHERE  exam.type       IS NULL       )
       AS info