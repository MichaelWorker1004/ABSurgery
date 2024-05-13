CREATE PROCEDURE [dbo].[get_picklist_states_bycountry]
@CountryCode CHAR(3)
AS
 select 
    country,
    state as ItemValue,
    state_description as ItemDescription    
 from 
    [dbo].[states_info]
 where 
    country = @CountryCode AND
    country_description!='US MILITARY'