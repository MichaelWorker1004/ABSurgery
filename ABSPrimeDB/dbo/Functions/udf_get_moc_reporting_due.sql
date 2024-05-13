CREATE FUNCTION [dbo].[udf_get_moc_reporting_due] (@candidate char(6)) 
RETURNS datetime
AS
begin

DECLARE 
@extension_due datetime,
@next_reporting_due datetime

SELECT  
	@extension_due=extension_due,
	@next_reporting_due=
	(case 
		when v_abms_cert.CertificationExpireDate!='' AND v_abms_cert.CertificationExpireDate<current_cycle_due THEN v_abms_cert.CertificationExpireDate
		when ([num_cycles_non_compliant]=(0) AND current_cycle_due>getdate() AND track.year IS NULL) 
			OR [num_cycles_non_compliant]>(0) 
			OR track.year IS NULL
		then CONVERT([datetime],CONVERT([varchar],YEAR(current_cycle_due),0)+'-12-31',0) 
		when v_abms_cert.CertificationExpireDate!='' AND v_abms_cert.CertificationExpireDate<DATEADD(year, 5, current_cycle_due) THEN v_abms_cert.CertificationExpireDate
		else CONVERT([datetime],CONVERT([varchar],YEAR(current_cycle_due)+(5),0)+'-12-31',0)
	end) 
FROM moc_eligibility  
	LEFT JOIN (select max(year) year,candidate from track where exam='M' and type='C' and app_receive IS NOT NULL group by candidate ) track on track.candidate=moc_eligibility.candidate and track.year >= moc_eligibility.current_year-1
	LEFT JOIN (select max(CertificationExpireDate) CertificationExpireDate,candidate from v_abms_cert group by candidate) v_abms_cert ON v_abms_cert.candidate=moc_eligibility.candidate
	where moc_eligibility.candidate=@candidate

SET @next_reporting_due=IIF(@extension_due IS NOT NULL,@extension_due,@next_reporting_due)

RETURN 
	IIF(@next_reporting_due='2021-12-31','2022-12-31',@next_reporting_due)
END