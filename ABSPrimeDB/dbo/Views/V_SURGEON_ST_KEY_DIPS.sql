CREATE VIEW dbo.V_SURGEON_ST_KEY_DIPS
AS
SELECT
                dbo.udf_get_namelast(candidate_ss)      ln,
                dbo.udf_get_namelastfirst(candidate_ss) lnfn,
                userid_ss                               userid,
                candidate_ss                            candidate,
                dbo.udf_get_email(candidate_ss) email,
                source.*,
                ISNULL(CAST(moc_eligibility.cohort AS VARCHAR), '')                  cohort,
                ISNULL(moc_eligibility.examcode, '')                                 examcode,
                ISNULL(CONVERT(VARCHAR, moc_eligibility.seniority_date, 120), '')    seniority_date,
                ISNULL(moc_eligibility.current_cycle, '')                            current_cycle,
                ISNULL(moc_eligibility.current_trackcode, '')                        current_trackcode,
                ISNULL(CONVERT(VARCHAR, moc_eligibility.current_cycle_due, 120), '') current_cycle_due,
                ISNULL(moc_eligibility.num_cycles_non_compliant, '')                 num_cycles_non_compliant,
                ISNULL(moc_eligibility.note, '')                                     note
FROM
                (
                                SELECT
                           mcodes.code,
                           mcodes.descr,
                           ss.UserId userid_ss,
                           ss.candidate candidate_ss
                FROM
                           mcodes
                           INNER JOIN
                                      surgeon_st ss
                           ON
                                      mcodes.code = ss.status_code
                WHERE
                           mcodes.grp          = 'SC'
                AND        mcodes.descr NOT LIKE '%invalid after%'
                AND
                           (
                           mcodes.descr LIKE '%member%'
                OR         mcodes.descr LIKE '%committee%'
                OR         mcodes.descr LIKE '%staff%'
                OR         mcodes.descr LIKE '%council%'
                OR         mcodes.descr LIKE '%board%'
                           )
                AND        ss.end_date > GETDATE()
                                
                UNION
                
                SELECT
                       *
                FROM
                       ( SELECT
                                       'SM'                   AS code,
                                       'Senior Board Members' AS descr,
                                       ss.userid                 userid_ss,
                                       ss.candidate              candidate_ss
                       FROM
                                       surgeon_st ss
                                       LEFT OUTER JOIN
                                                       surgeon_st de
                                       ON
                                                       ss.UserId   = de.UserId
                                       AND             de.status_code = 'DE'                        WHERE
                                       ss.status_code           in ('AM','BD')
                       AND             ss.end_date              < GETDATE()
                       AND             ISNULL(de.UserId, '') = ''
                       ) AS sm
                
                UNION
                
                                SELECT
                       *
                FROM
                       ( SELECT
                                       'NM'                AS code,
                                       'New Members' AS descr,
                                       ss.userid              userid_ss,
                                       ss.candidate           candidate_ss
                       FROM
                                       surgeon_st ss
                                       LEFT OUTER JOIN
                                                       surgeon_st de
                                       ON
                                                       ss.UserId   = de.UserId
                                       AND             de.status_code = 'DE'                        WHERE
                                       ss.status_code           IN ('AM', 'BD', 'CC', 'GI', 'GS', 'IT', 'PS', 'SO', 'TB', 'TR', 'VS')
                       AND             ss.start_date            > GETDATE()
                       AND             ISNULL(de.UserId, '') = ''
                       ) AS sm
                
                UNION
                
                                SELECT
                       'CE'                                   AS code,
                       'General Surgery Certifying Examiners' AS descr,
                       info.userid                               userid_ss,
                       info.candidate                            candidate_ss
                FROM
                       ( SELECT
                                       s.UserId,
                                       s.candidate,
                                       CONVERT(CHAR(10), s.birthdate, 126) birthdate,
                                       CASE
                                                       WHEN s.birthdate = ''
                                                       THEN ''
                                                       ELSE
                                                             (SELECT TOP 1
                                                                     Age
                                                             FROM
                                                                     dbo.udf_get_age(CAST(birthdate AS DATETIME), CURRENT_TIMESTAMP)
                                                             )
                                       END AS AGE_TODAY,
                                       CASE
                                                       WHEN s.birthdate = ''
                                                       THEN ''
                                                       ELSE
                                                             (SELECT TOP 1
                                                                     Age
                                                             FROM
                                                                     dbo.udf_get_age(CAST(birthdate AS DATETIME), start_of_ay)
                                                             )
                                       END AS AGE_START_OF_AY
                       FROM
                                       (SELECT
                                               UserId,
                                               candidate,
                                               ISNULL(birthdate, ' ') AS birthdate
                                       FROM
                                               surgeon ss
                                       ) s
                                       LEFT OUTER JOIN
                                                       surgeon_st ss
                                       ON
                                                       s.UserId = ss.UserId
                                       LEFT OUTER JOIN
                                                       (SELECT
                                                               attr + '-07-01' start_of_ay
                                                       FROM
                                                               mcodes
                                                       WHERE
                                                               grp  = 'AY'
                                                       AND     code = 'E'
                                                       ) ay
                                       ON
                                                       1 = 1
                                       LEFT OUTER JOIN
                                                       surgeon_st de
                                       ON
                                                       s.UserId    = de.UserId
                                       AND             de.status_code = 'DE'                        WHERE
                                       ss.status_code           in  ('AM','BD') AND
                                       s.candidate              not like ('%P%')
                       AND             ISNULL(de.UserId, '') = ''
                       ) AS info
                WHERE
                       AGE_START_OF_AY <> ''
                AND    AGE_START_OF_AY  < '65y'
                AND    AGE_START_OF_AY  > '12'
                ) AS SOURCE
                LEFT OUTER JOIN
                                moc_eligibility
                ON
                                source.userid_ss = moc_eligibility.UserId