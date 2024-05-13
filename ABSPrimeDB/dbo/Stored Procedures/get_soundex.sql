create proc get_soundex
   @_string varchar ( 20 )
as  
        select last_name from surgeon where soundex(last_name) = soundex(@_string)
