CREATE  VIEW dbo.V_789_10P
AS
select
	candidate,exam,expiration,namelast,addresslookup,addresstype,addressblock 
from V_789_MAIN
	where expiration in (2009) and exam='P' and director not like '%Y%'
