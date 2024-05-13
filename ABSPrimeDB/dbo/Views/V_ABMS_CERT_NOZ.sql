create VIEW [dbo].[V_ABMS_CERT_NOZ] AS
select * from [V_ABMS_CERTALL_NOZ] 
	where istop=1