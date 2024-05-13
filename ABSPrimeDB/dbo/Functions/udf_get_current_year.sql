CREATE FUNCTION [dbo].[udf_get_current_year] (@cohort smallint)
RETURNS smallint
AS
BEGIN
	DECLARE @ay int
	select @ay=CONVERT([smallint],attr,(0)) from mcodes where grp='AY' and code='M'
	return (
		((@ay-@cohort-1)/5+case when (@cohort+6)>@ay then 1 else 0 end)*5 + @cohort + 2
	)			
END