CREATE FUNCTION [dbo].[udf_get_cme_exam_mc] (@candidate char(6),@app_type as varchar(6),@cme_from smalldatetime,@cme_to smalldatetime)
RETURNS float
AS
BEGIN
	DECLARE @result float
	select @result=isnull(sum(hours),0) from cme
	    inner join moc_eligibility on moc_eligibility.candidate=cme.candidate
	where 
		isnull(usedwith,'') in ('true','',@app_type) 
		and (releasedate between @cme_from and @cme_to or 
			(inv_num like moc_eligibility.candidate+rtrim(moc_eligibility.examcode)+'%' and datediff(day,seniority_date,@cme_from)<367)
		) 
		and ReportingOrganization='ABS'
		and cme.candidate=@candidate
	RETURN ISNULL(@result,0)
END