create proc get_school
as
    select description + ' - ' + type  from school
