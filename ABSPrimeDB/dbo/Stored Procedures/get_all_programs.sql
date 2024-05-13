create proc get_all_programs
        @flag char(1)
as  
  if @flag = 'N'
  begin
          select number, abbname from program order by number
  end
  else
  begin
          select number, abbname from program order by abbname
  end
