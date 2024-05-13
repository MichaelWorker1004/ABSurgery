CREATE  VIEW dbo.V_789_10C
AS
select
	candidate,exam,expiration,namelast,addresslookup,addresstype,addressblock 
from V_789_MAIN
	where expiration in (2009) and exam='C' and director not like '%Y%'
