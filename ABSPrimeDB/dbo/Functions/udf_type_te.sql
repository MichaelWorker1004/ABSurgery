CREATE FUNCTION [dbo].[udf_type_te] ( @exam char(1),@type char(1))
RETURNS char(1)
AS
BEGIN
declare @newtype char(1)
select @newtype=(case @exam+@type
	when 'CC' then 'W'
	when 'CO' then 'W'
	when 'ZC' then 'W'
	when 'ZO' then 'W'
	when 'GC' then 'O'
	when 'GQ' then 'W'
	when 'HC' then 'W'
	when 'HO' then 'W'
	when 'PC' then 'O'
	when 'PQ' then 'W'
	when 'VC' then 'O'
	when 'VQ' then 'W'
	when 'VO' then 'W'
	when 'OC' then 'O'
	when 'OQ' then 'W'
	else @type end)

return @newtype
end
