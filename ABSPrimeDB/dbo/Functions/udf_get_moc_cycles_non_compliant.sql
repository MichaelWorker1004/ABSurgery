CREATE FUNCTION dbo.udf_get_moc_cycles_non_compliant(@candidate char(6))
RETURNS tinyint
AS
BEGIN
    DECLARE @num_cycles_non_compliant tinyint;
    SELECT @num_cycles_non_compliant = 
    case 
    when track.year is not null or cmoc_cohort is not null then 0
    else 1
    end
    from moc_eligibility
    LEFT JOIN (select max(year) year,candidate from track where exam='M' and type='C' and app_receive IS NOT NULL group by candidate ) track on track.candidate=moc_eligibility.candidate and track.year >= (select attr from mcodes where grp='AY' and code='M')-5
    where moc_eligibility.candidate=@candidate
RETURN @num_cycles_non_compliant
END
GO