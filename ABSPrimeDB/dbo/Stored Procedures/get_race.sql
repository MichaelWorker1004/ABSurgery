create proc get_race
as
    select description + ' - ' + type  from race order by description desc
