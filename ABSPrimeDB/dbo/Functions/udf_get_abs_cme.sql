CREATE FUNCTION [dbo].[udf_get_abs_cme] (@candidate char(6),@app_type as varchar(6),@cme_from smalldatetime,@cme_to smalldatetime)
RETURNS @abstable TABLE
   (
		abshtml	varchar(max),
		abshours	float,
		abshours_sa float
   )
AS
BEGIN
DECLARE @abshtml varchar(max),@abshours float,@abshours_sa float, @abshtml_temp varchar(max),@abshours_temp float,@abshours_sa_temp float

set @abshtml=''
set @abshours=0
set @abshours_sa=0
set @abshtml_temp=''
set @abshours_temp=0
set @abshours_sa_temp=0

DECLARE abs_cursor CURSOR FOR 

	select '<tr class=''blue2''><td class=''tablecontent''>'+ActivityName +'</td>'+
		'<td class=''number''>'+cast(FORMAT(isnull(hours,0)*-1, '0.0;-0.0') as varchar)+'</td><td class=''number''>'+cast(FORMAT(isnull(credits_sa,0)*-1, '0.0;-0.0') as varchar)+'</td></tr>' abshtml,isnull(hours,0) hours,isnull(credits_sa, 0) credits_sa
		from moc_eligibility
		    inner join cme on moc_eligibility.candidate=cme.candidate
		where 
			(releasedate between @cme_from and @cme_to or 
				(inv_num like moc_eligibility.candidate+rtrim(moc_eligibility.examcode)+'%' and datediff(day,seniority_date,@cme_from)<367)
			) 
			and ReportingOrganization='ABS'
			and cme.candidate=@candidate
		order by inv_num

OPEN abs_cursor
FETCH NEXT FROM abs_cursor INTO @abshtml_temp,@abshours_temp,@abshours_sa_temp
WHILE @@FETCH_STATUS = 0
BEGIN
	set @abshtml=@abshtml+@abshtml_temp
	set @abshours=@abshours+@abshours_temp
	set @abshours_sa=@abshours_sa+@abshours_sa_temp
	FETCH NEXT FROM abs_cursor INTO @abshtml_temp,@abshours_temp,@abshours_sa_temp
END

CLOSE abs_cursor
DEALLOCATE abs_cursor

INSERT @abstable select @abshtml,@abshours,@abshours_sa
RETURN

END