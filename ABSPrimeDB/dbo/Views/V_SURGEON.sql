CREATE VIEW V_SURGEON
AS 

SELECT min(ssn) ssn,
	min(Board_Unique_UserID) UserId,
	min(Board_Unique_ID) candidate,
	min([Last Name]) namelast,
	min([First Name]) namefirst,
	min([Last Name]+', '+[First Name]+' '+left([Middle Name],1)) namelastfirst,
	min([First Name]+' '+ CASE isnull([Middle Name], '') WHEN '' THEN '' ELSE left([Middle Name],1)+'. ' END +[Last Name]) namefirstlast,
	min(surgeon.[First Name] + ' ' + (CASE Len(surgeon.[Middle Name]) WHEN 0 THEN '' ELSE surgeon.[Middle Name] + ' ' END) + surgeon.[Last Name]+ (CASE Len(surgeon.[Suffix]) WHEN 0 THEN '' ELSE ', ' + surgeon.[Suffix] + '' END)+
		(CASE (surgeon.[Degree]) WHEN 'MD' THEN ', M.D.' WHEN 'MBBS' THEN ', M.B.B.S.' WHEN 'DO' THEN ', D.O.'  when 'MBChB' then ', M.B.Ch.B'  END)) nameprint,
	
	min(surgeon.[First Name] + ' ' + (CASE Len(surgeon.[Middle Name]) WHEN 0 THEN '' ELSE surgeon.[Middle Name] + ' ' END) + surgeon.[Last Name]+ (CASE Len(surgeon.[Suffix]) WHEN 0 THEN '' ELSE ', ' + surgeon.[Suffix] + '' END)+
		(CASE (surgeon.[Degree]) WHEN 'MD' THEN ', M.D.' WHEN 'MBBS' THEN ', M.B.B.S.' WHEN 'DO' THEN ', D.O.' ELSE '' END) + char(13) + char(10) +  
		(CASE isnull(ltrim(rtrim(title)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(title)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(institution)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(institution)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(street1)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street1)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(street2)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(street2)), '') + char(13) + char(10) END) + 
		(CASE isnull(ltrim(rtrim(city)), '') WHEN '' THEN '' ELSE ltrim(rtrim(city)) + ', ' END) + 
		(CASE isnull(ltrim(rtrim(state)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(state)), '') + ' ' END) + 
		(CASE isnull(ltrim(rtrim(zip)), '') WHEN '' THEN '' ELSE isnull(ltrim(rtrim(zip)), '') + char(13) + char(10) END) + 
		(CASE isnull(street4, '') WHEN '' THEN '' ELSE isnull(street4, '') END)) AS addressblock,


	min(left(replace(replace(replace(replace(replace(isnull(b.number,''),'-',''),')',''),'(',''),' ',''),'.',''),10)) phone,
	min(left(replace(replace(replace(replace(replace(isnull(c.number,''),'-',''),')',''),'(',''),' ',''),'.',''),10)) fax,
 	min(isnull(d.number,'')) email 
FROM	V_ABMS_SURGEON surgeon
left outer join address a on a.code=surgeon.Board_Unique_ID and a.mail='M'
left outer join phone b on a.type=b.type1 and a.code=b.code and b.type2='V'
left outer join phone c on a.type=c.type1 and a.code=c.code and c.type2='F'
left outer join phone d on surgeon.Board_Unique_ID=d.code and d.number like '%@%'
group by surgeon.Board_Unique_ID, surgeon.Board_Unique_UserID





