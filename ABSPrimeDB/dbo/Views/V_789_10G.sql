CREATE  VIEW dbo.V_789_10G
AS
select
	candidate,exam,expiration,namelast,addresslookup,addresstype,addressblock 
from V_789_MAIN
	where expiration in (2009) and exam='G' and director not like '%Y%'
