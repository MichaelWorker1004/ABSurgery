CREATE VIEW [dbo].[V_ABMS_CERT] AS
select * from [V_ABMS_CERTALL] 
	where istop=1