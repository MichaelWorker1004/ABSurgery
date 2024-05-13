CREATE FUNCTION udf_get_cme_abscredit (@candidate char(6))
RETURNS int
AS
BEGIN
	begin
		DECLARE @cme_id int
		
		select @cme_id=id from cme where candidate=@candidate and ReportingOrganization='ABS' and inv_num like 
			(select candidate+rtrim(examcode)+'%' from moc_eligibility where candidate=@candidate)
	end
	RETURN isnull(@cme_id,0)
END