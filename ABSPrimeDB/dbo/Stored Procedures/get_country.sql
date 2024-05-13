create proc get_country
as
    select description + ' - ' + code  from country order by sort_code, description
