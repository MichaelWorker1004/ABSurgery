CREATE FUNCTION udf_get_addressblock ( @code varchar(7))
RETURNS varchar(1000)
AS
BEGIN
declare @addressblock varchar(1000)

if len(rtrim(@code))=6
begin
	SELECT @addressblock =   
		rtrim(ltrim([First Name] + ' ' + (CASE Len([Middle Name]) WHEN 0 THEN '' ELSE [Middle Name] + ' ' END) + [Last Name]))+ (CASE Len([Suffix]) WHEN 0 THEN '' ELSE ', ' + [Suffix] + '' END)+
		(CASE ([Degree]) WHEN 'MD' THEN ', M.D.' WHEN 'MBBS' THEN ', M.B.B.S.' WHEN 'DO' THEN ', D.O.' WHEN 'MBChB' THEN ', M.B.Ch.B' ELSE '' END) + char(13) + char(10) +  
		(CASE isnull(ltrim(rtrim(title)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(title)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(institution)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(institution)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(street1)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street1)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(street2)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street2)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(city)), '') WHEN '' THEN '' ELSE ltrim(rtrim(city)) + ', ' END) + 
		(CASE isnull(ltrim(rtrim(state)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(state)), '') + ' ' END) + 
		(CASE isnull(ltrim(rtrim(zip)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(zip)), '') + char(13) + char(10) END) + 
		(CASE RTRIM(isnull(street4, '')) WHEN '' THEN '' WHEN 'United States' THEN '' WHEN 'USA' THEN '' ELSE isnull(street4, '') END) 
	FROM address
		INNER JOIN v_abms_surgeon as surgeon ON address.code = surgeon.board_unique_id
	WHERE address.mail = 'M' and address.code = @code and address.code = surgeon.board_unique_id
end
else
begin
	if right(@code,1)='S' 	begin
                select @addressblock = (SELECT     
                        isnull(first_name, '') + ' ' + 
                        CASE isnull(middle_name, '') WHEN '' THEN '' ELSE middle_name + ' ' END + 
                        isnull(last_name, '') + 
                        CASE isnull(suffix, '') WHEN '' THEN '' ElSE ', ' + suffix END +
                        CASE isnull(degreetitle, '') WHEN '' THEN '' ElSE ', ' + degreetitle END + char(13) + char(10) +
                        (CASE isnull(ltrim(rtrim(institution)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(institution)), '') + char(13) + char(10) END) + 
                        (CASE isnull(ltrim(rtrim(street1)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street1)), '') + char(13) + char(10) END) + 
                        (CASE isnull(ltrim(rtrim(street2)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street2)), '') + char(13) + char(10) END) + 
                        (CASE isnull(ltrim(rtrim(city)), '') WHEN '' THEN '' ELSE ltrim(rtrim(city)) + ', ' END) + 
                        (CASE isnull(ltrim(rtrim(state)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(state)), '') + ' ' END) + 
                        (CASE isnull(ltrim(rtrim(zip)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(zip)), '') + char(13) + char(10) END) + 
                        (CASE RTRIM(isnull(country, '')) WHEN '' THEN '' WHEN 'United States' THEN '' WHEN 'USA' THEN '' ELSE isnull(country, '') END) 
                        AS address_block
		FROM	abmsdata
		WHERE	abscode=left(@code,6))
	end
	else if right(@code,1)='C' 	begin
		select @addressblock = (SELECT     
			surgeon.[Last Name] + ', ' + surgeon.[First Name]+' '+(CASE Len(surgeon.[Middle Name]) WHEN 0 THEN '' ELSE surgeon.[Middle Name] + ' ' END) + (CASE Len(surgeon.[Suffix]) WHEN 0 THEN '' ELSE ', ' + surgeon.[Suffix] + '' END)+
				+space(5)+ left(@code,6) + char(13) + char(10)+
			(CASE isnull(ltrim(rtrim(title)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(title)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(institution)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(institution)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(street1)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street1)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(street2)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street2)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(city)), '') WHEN '' THEN '' ELSE ltrim(rtrim(city)) + ', ' END) + 
			(CASE isnull(ltrim(rtrim(state)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(state)), '') + ' ' END) + 
			(CASE isnull(ltrim(rtrim(zip)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(zip)), '') + char(13) + char(10) END) + 
			(CASE RTRIM(isnull(street4, '')) WHEN '' THEN '' WHEN 'United States' THEN '' WHEN 'USA' THEN '' ELSE isnull(street4, '') END) AS address_block
		FROM	address, v_abms_surgeon as surgeon 
		WHERE	address.mail = 'M'  and 
			address.code = left(@code,6) and 
			address.code = surgeon.board_unique_id)
	end
	else
	begin
		select @addressblock = (SELECT     
			surgeon.[First Name] + ' ' + (CASE Len(surgeon.[Middle Name]) WHEN 0 THEN '' ELSE surgeon.[Middle Name] + ' ' END) + surgeon.[Last Name]+ (CASE Len(surgeon.[Suffix]) WHEN 0 THEN '' ELSE ', ' + surgeon.[Suffix] + '' END)+
			(CASE (surgeon.[Degree]) WHEN 'MD' THEN ', M.D.' WHEN 'MBBS' THEN ', M.B.B.S.' WHEN 'DO' THEN ', D.O.' WHEN 'MBChB' THEN ', M.B.Ch.B' ELSE '' END) + char(13) + char(10) +  
			(CASE isnull(ltrim(rtrim(title)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(title)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(institution)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(institution)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(street1)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street1)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(street2)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street2)), '') + char(13) + char(10) END) + 
			(CASE isnull(ltrim(rtrim(city)), '') WHEN '' THEN '' ELSE ltrim(rtrim(city)) + ', ' END) + 
			(CASE isnull(ltrim(rtrim(state)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(state)), '') + ' ' END) + 
			(CASE isnull(ltrim(rtrim(zip)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(zip)), '') + char(13) + char(10) END) + 
			(CASE RTRIM(isnull(street4, '')) WHEN '' THEN '' WHEN 'United States' THEN '' WHEN 'USA' THEN '' ELSE isnull(street4, '') END) AS address_block
		FROM	address, v_abms_surgeon as surgeon 
		WHERE	address.type = right(@code,1) and 
			address.code = left(@code,6) and 
			address.code = surgeon.board_unique_id)
	end
end

return isnull(@addressblock,'')
end