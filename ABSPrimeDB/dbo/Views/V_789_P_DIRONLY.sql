CREATE  VIEW dbo.V_789_P_DIRONLY
AS
select
	candidate,exam,expiration,namelast,addresslookup,addresstype,addressblock 
from V_789_MAIN
	where expiration in (2010,2011,2012) and exam='P' and director like '%Y%'

