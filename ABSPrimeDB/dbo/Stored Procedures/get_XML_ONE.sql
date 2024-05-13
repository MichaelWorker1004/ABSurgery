CREATE proc get_XML_ONE
@last_year as smallint

as
select board_unique_id as cand,ssn,[first name] as fname,
[middle name] as mname,
[last name] as lname,address1 as addr1,address2 as addr2,
address3 as addr3,address4 as addr4,city,[state or province] as state,
isnull(case country when 'USA' then [zip code] else [foreign zip code] end,'') as zip,
country,replace(birthdate,'/','') as bdate,exam,type,year,
app_rcvd=(case year(isnull(app_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),app_rcvd,101),'') end),
app_appr=(case year(isnull(app_appr,'')) when 1900 then '' else isnull(convert(varchar(10),app_appr,101),'') end),
appfee_rcvd=(case year(isnull(appfee_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),appfee_rcvd,101),'') end),
rply_sent=(case year(isnull(rply_sent,'')) when 1900 then '' else isnull(convert(varchar(10),rply_sent,101),'') end),
rply_rcvd=(case year(isnull(rply_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),rply_rcvd,101),'') end),
adms_sent=(case year(isnull(adms_sent,'')) when 1900 then '' else isnull(convert(varchar(10),adms_sent,101),'') end),
regfee_rcvd=(case year(isnull(regfee_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),regfee_rcvd,101),'') end),
isnull(pref1,'') as pref1,isnull(pref2,'') as pref2,isnull(pref3,'') as pref3,isnull(pref4,'') as pref4,isnull(pref5,'') as pref5,isnull(pref6,'') as pref6,
isnull(pref_rcvd,'') as pref_rcvd,isnull(pf,'') as pf,
isnull(rtrim(ltrim(area)),'') as area,isnull(day,'') as day,isnull(briefing,'') as briefing, asgn_area=(mcodes.Descr),
 asgn_day=(datename(m, cast(attr as datetime))+' ' + case isnull(track.day, 0) when 0 then cast(Datepart(dd, cast(attr as datetime)) as varchar)+'-'+cast(datepart(dd, dateadd(day, 2, cast(attr as datetime))) as varchar) else cast(datepart(dd, dateadd(day, track.day-1, cast(attr as datetime))) as varchar) end +', ' +cast(datepart(yyyy, cast(attr as datetime)) as varchar)),
 asgn_briefing=(case isnull(briefing, '') when '' then '' when 1 then '7:00 AM' when 2 then '11:30 AM' end),
status 
from v_abms_address as info 
inner join V_EXAM_TRACK track on track.cand=info.board_unique_id and track.year>@last_year left outer join
 mcodes on mcodes.grp = 'EC' and cast(track.year as varchar(4))+'GO'+cast(track.area as varchar(1)) = mcodes.code
where ssn <> '' order by board_unique_id for xml auto,elements