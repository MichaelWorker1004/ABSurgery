CREATE FUNCTION [dbo].[udf_type_et] ( @exam char(1),@type char(1))
RETURNS char(1)
AS
BEGIN
declare @newtype char(1)
select @newtype=(case @exam+@type
	when 'CW' Then 'C' 
	when 'CO' Then 'C' 
	when 'ZW' Then 'C' 
	when 'ZO' Then 'C' 
	when 'GO' then 'C' 
	when 'GW' then 'Q' 
	when 'HW' then 'C' 
	when 'HO' then 'C' 
	when 'PO' then 'C' 
	when 'PW' then 'Q' 
	when 'VO' then 'C' 
	when 'VW' then 'Q'
	when 'OO' then 'C' 
	when 'OW' then 'Q'	
	else @type end)

return @newtype
end