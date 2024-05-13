--ead
--bcp "select * from ABS..V_PEARSON_EAD order by ClientCandidateID desc" queryout "c:\result.txt" /S MIXMASTER -T -c -e"c:\error.txt"
CREATE VIEW dbo.V_PEARSON_EAD_UPDT
AS
SELECT     'U' AS AuthorizationTransactionType, '' AS AuthorizationID,'ABS'+candidate+exam+type+cast(year as varchar)+cast(isnull(attempt,0) as varchar) AS ClientAuthorizationID, 'ABS0' + candidate AS ClientCandidateID, '1' AS ExamAuthorizationCount, 
		substring(a.descr,charindex('(',a.descr)+1,charindex(')',a.descr)-charindex('(',a.descr)-1)  ExamSeriesCode,
		CASE isnull(a.attr,'') when '' then '*ERROR*' else cast(year(a.attr) as varchar)+'/'+right('0'+cast(datepart(mm,a.attr) as varchar),2)+'/'+right('0'+cast(datepart(dd,a.attr) as varchar),2)+' 00:00:00' end EligibilityApptDateFirst,
		CASE isnull(a.attr2,'') when '' then '*ERROR*' else cast(year(a.attr2) as varchar)+'/'+right('0'+cast(datepart(mm,a.attr2) as varchar),2)+'/'+right('0'+cast(datepart(dd,a.attr2) as varchar),2)+' 23:59:59' end EligibilityApptDateLast,
		replace(CONVERT(varchar(20),getdate(), 120), '-', '/') AS LastUpdate
FROM         exam
left outer join mcodes a on a.code=cast(year as varchar)+exam+type and a.grp='EC'
WHERE descr like '%(%' 
