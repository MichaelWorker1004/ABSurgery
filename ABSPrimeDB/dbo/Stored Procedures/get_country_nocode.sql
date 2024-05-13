create proc get_country_nocode
as
    select description from country order by sort_code, description
