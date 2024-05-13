CREATE view dbo.temp_roster_letters
as
select 
 number+exam as progcode, 
 dbo.udf_get_programaddressblock(number+exam) addressblock,
 program.abbname,
 program.last,
 program.pwd,
 dbo.udf_get_email(number+exam) as pd_email,
 program.crd_email
from program where exam='G' and approval='A' 
and number+exam in 
('1558G',
'9000G',
'9001G',
'9009G',
'0041G',
'0113G',
'0166G',
'0389G',
'0506G',
'0469G',
'0727G',
'0826G',
'1318G',
'1623G')