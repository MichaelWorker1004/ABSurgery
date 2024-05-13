CREATE proc get_pearson_ead
	@date smalldatetime
as 
set nocount on

/*if datename(dw,@date)='Friday'
begin
	set @adms_sent_date=dateadd(day,3,@date)
end
else
begin
	set @adms_sent_date=dateadd(day,1,@date)
end
*/
update exam set pearson_sent=@date --,adms_sent=@adms_sent_date
WHERE exam + type IN (SELECT DISTINCT RIGHT(Code, 2)  FROM mcodes WHERE Grp = 'EC'  and  descr like '%(%' and  descr like '%-%') AND status = 'R' and pearson_sent is null and pearson_auth_id is null
SELECT     'A' AS AuthorizationTransactionType, '' AS AuthorizationID,'ABS'+exam.candidate+exam+type+cast(year as varchar)+cast(isnull(attempt,0) as varchar) AS ClientAuthorizationID, 'ABS0' + exam.candidate AS ClientCandidateID, '1' AS ExamAuthorizationCount, 
		substring(a.descr,charindex('(',a.descr)+1,charindex(')',a.descr)-charindex('(',a.descr)-1) ExamSeriesCode,
		CASE isnull(a.attr,'') when '' then '*ERROR*' else cast(year(a.attr) as varchar)+'/'+right('0'+cast(datepart(mm,a.attr) as varchar),2)+'/'+right('0'+cast(datepart(dd,a.attr) as varchar),2)+' 00:00:00' end EligibilityApptDateFirst,
		CASE isnull(a.attr2,'') when '' then '*ERROR*' else cast(year(a.attr2) as varchar)+'/'+right('0'+cast(datepart(mm,a.attr2) as varchar),2)+'/'+right('0'+cast(datepart(dd,a.attr2) as varchar),2)+' 23:59:59' end EligibilityApptDateLast,
		isnull(ada.codes, '') AS Accommodations,
		replace(CONVERT(varchar(20),getdate(), 120), '-', '/') AS LastUpdate
FROM         exam
left outer join mcodes a on a.code=cast(year as varchar)+exam+type and a.grp='EC'
left outer join (
	SELECT candidate,
		st_val,
		LEFT(code,Len(code)-1) As "codes"
	FROM
    (
 		SELECT DISTINCT ST2.candidate, ST2.st_val,
		(
			SELECT substring(status_code, 5, 10) + '*' AS [text()]
			FROM surgeon_st ST1
			WHERE ST1.candidate = ST2.candidate
			and ST1.st_val=ST2.st_val
			and status_code LIKE 'ADA%'
			ORDER BY ST1.candidate
			FOR XML PATH (''), TYPE
		).value('text()[1]','nvarchar(max)') [code]
		FROM surgeon_st ST2
		WHERE status_code LIKE 'ADA%'
	) [Main]
) ada on ada.candidate=exam.candidate  and exam.examcode=ada.st_val
left outer join surgeon_st a1a2 on a1a2.candidate=exam.candidate and a1a2.st_val=exam.examcode and a1a2.status_code='A1A2'
WHERE descr like '%(%' and  pearson_sent = @date and a1a2.status_code IS NULL
UNION
SELECT     'A' AS AuthorizationTransactionType, '' AS AuthorizationID,'ABS'+exam.candidate+exam+type+cast(year as varchar)+cast(isnull(attempt,0)+1 as varchar) AS ClientAuthorizationID, 'ABS0' + exam.candidate AS ClientCandidateID, '1' AS ExamAuthorizationCount, 
		substring(a.descr,charindex('(',a.descr)+1,charindex(')',a.descr)-charindex('(',a.descr)-1)+'-A1' ExamSeriesCode,
		CASE isnull(a.attr,'') when '' then '*ERROR*' else cast(year(a.attr) as varchar)+'/'+right('0'+cast(datepart(mm,a.attr) as varchar),2)+'/'+right('0'+cast(datepart(dd,a.attr) as varchar),2)+' 00:00:00' end EligibilityApptDateFirst,
		CASE isnull(a.attr2,'') when '' then '*ERROR*' else cast(year(a.attr2) as varchar)+'/'+right('0'+cast(datepart(mm,a.attr2) as varchar),2)+'/'+right('0'+cast(datepart(dd,a.attr2) as varchar),2)+' 23:59:59' end EligibilityApptDateLast,
		isnull(ada.codes, '') AS Accommodations,
		replace(CONVERT(varchar(20),getdate(), 120), '-', '/') AS LastUpdate
FROM         exam
left outer join mcodes a on a.code=cast(year as varchar)+exam+type and a.grp='EC'
left outer join (
	SELECT candidate,
		st_val,
		LEFT(code,Len(code)-1) As "codes"
	FROM
    (
 		SELECT DISTINCT ST2.candidate, ST2.st_val,
		(
			SELECT substring(status_code, 5, 10) + '*' AS [text()]
			FROM surgeon_st ST1
			WHERE ST1.candidate = ST2.candidate
			and ST1.st_val=ST2.st_val
			and status_code LIKE 'ADA%'
			ORDER BY ST1.candidate
			FOR XML PATH (''), TYPE
		).value('text()[1]','nvarchar(max)') [code]
		FROM surgeon_st ST2
		WHERE status_code LIKE 'ADA%'
	) [Main]
) ada on ada.candidate=exam.candidate  and exam.examcode=ada.st_val
left outer join surgeon_st a1a2 on a1a2.candidate=exam.candidate and a1a2.st_val=exam.examcode and a1a2.status_code='A1A2'
WHERE descr like '%(%' and  pearson_sent = @date and a1a2.status_code IS NOT NULL
UNION
SELECT     'A' AS AuthorizationTransactionType, '' AS AuthorizationID,'ABS'+exam.candidate+exam+type+cast(year as varchar)+cast(isnull(attempt,0)+2 as varchar) AS ClientAuthorizationID, 'ABS0' + exam.candidate AS ClientCandidateID, '1' AS ExamAuthorizationCount, 
		substring(a.descr,charindex('(',a.descr)+1,charindex(')',a.descr)-charindex('(',a.descr)-1)+'-A2' ExamSeriesCode,
		CASE isnull(a.attr,'') when '' then '*ERROR*' else cast(year(dateadd(day, 1, a.attr)) as varchar)+'/'+right('0'+cast(datepart(mm,dateadd(day, 1, a.attr)) as varchar),2)+'/'+right('0'+cast(datepart(dd,dateadd(day, 1, a.attr)) as varchar),2)+' 00:00:00' end EligibilityApptDateFirst,
		CASE isnull(a.attr2,'') when '' then '*ERROR*' else cast(year(dateadd(day, 1, a.attr2)) as varchar)+'/'+right('0'+cast(datepart(mm,dateadd(day, 1, a.attr2)) as varchar),2)+'/'+right('0'+cast(datepart(dd,dateadd(day, 1, a.attr2)) as varchar),2)+' 23:59:59' end EligibilityApptDateLast,
		isnull(ada.codes, '') AS Accommodations,
		replace(CONVERT(varchar(20),getdate(), 120), '-', '/') AS LastUpdate
FROM         exam
left outer join mcodes a on a.code=cast(year as varchar)+exam+type and a.grp='EC'
left outer join (
	SELECT candidate,
		st_val,
		LEFT(code,Len(code)-1) As "codes"
	FROM
    (
 		SELECT DISTINCT ST2.candidate, ST2.st_val,
		(
			SELECT substring(status_code, 5, 10) + '*' AS [text()]
			FROM surgeon_st ST1
			WHERE ST1.candidate = ST2.candidate
			and ST1.st_val=ST2.st_val
			and status_code LIKE 'ADA%'
			ORDER BY ST1.candidate
			FOR XML PATH (''), TYPE
		).value('text()[1]','nvarchar(max)') [code]
		FROM surgeon_st ST2
		WHERE status_code LIKE 'ADA%'
	) [Main]
) ada on ada.candidate=exam.candidate  and exam.examcode=ada.st_val
left outer join surgeon_st a1a2 on a1a2.candidate=exam.candidate and a1a2.st_val=exam.examcode and a1a2.status_code='A1A2'
WHERE descr like '%(%' and  pearson_sent = @date and a1a2.status_code IS NOT NULL