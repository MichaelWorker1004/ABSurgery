CREATE FUNCTION [dbo].[udf_get_programaddressblock] ( @progcode char(5))
RETURNS varchar(1000)
AS
BEGIN
declare @addressblock varchar(1000)
select @addressblock = (SELECT     
	program.pd + char(13) + char(10) +  
	(CASE isnull(ltrim(rtrim(title)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(title)), '') + char(13) + char(10) END) + 
	(CASE isnull(ltrim(rtrim(institution)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(institution)), '') + char(13) + char(10) END) + 
	(CASE isnull(ltrim(rtrim(street1)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street1)), '') + char(13) + char(10) END) + 
	(CASE isnull(ltrim(rtrim(street2)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street2)), '') + char(13) + char(10) END) + 
	(CASE isnull(ltrim(rtrim(city)), '') WHEN '' THEN '' ELSE ltrim(rtrim(city)) + ', ' END) + 
	(CASE isnull(ltrim(rtrim(state)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(state)), '') + ' ' END) + 
	(CASE isnull(ltrim(rtrim(zip)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(zip)), '') + char(13) + char(10) END) + 
	(CASE isnull(street4, '') WHEN '' THEN '' ELSE isnull(street4, '') END) AS address_block
FROM	program left outer join address on address.code = program.number + program.exam
WHERE	address.mail = 'M'and 
	program.number + program.exam =@progcode)

return isnull(@addressblock,'')
end

