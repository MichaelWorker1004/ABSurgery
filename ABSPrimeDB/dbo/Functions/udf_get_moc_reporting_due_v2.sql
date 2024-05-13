CREATE FUNCTION [dbo].[udf_get_moc_reporting_due_v2] (@candidate char(6)) 
RETURNS datetime
AS
begin
DECLARE 
@extension_due datetime,
@next_reporting_due datetime;
SELECT  
	@next_reporting_due=
	(case 
		when track.year IS NULL
		then CONVERT([datetime],CONVERT([varchar],YEAR(getdate()),0)+'-12-31',0) 
		when v_abms_cert.CertificationExpireDate!='' AND v_abms_cert.CertificationExpireDate<DATEADD(year, 5, CAST(CAST(track.year as VARCHAR)+'-12-31'  as DATETIME)) THEN v_abms_cert.CertificationExpireDate
		else DATEADD(year, 5, CAST(CAST(track.year as VARCHAR)+'-12-31'  as DATETIME))
	end) 
FROM moc_eligibility  
	LEFT JOIN (select max(year) year,candidate from track where exam='M' and type='C' and app_receive IS NOT NULL group by candidate ) track on track.candidate=moc_eligibility.candidate and track.year >= (select attr from mcodes where grp='AY' and code='M')-5
	LEFT JOIN (select min(CertificationExpireDate) CertificationExpireDate,candidate from v_abms_cert where CertificationExpireDate>=GETDATE() group by candidate) v_abms_cert ON v_abms_cert.candidate=moc_eligibility.candidate
	where moc_eligibility.candidate=@candidate
SET @next_reporting_due=IIF(@next_reporting_due<getdate(),CONVERT([datetime],CONVERT([varchar],YEAR(getdate()),0)+'-12-31',0) ,@next_reporting_due)
RETURN 
	@next_reporting_due
END
GO