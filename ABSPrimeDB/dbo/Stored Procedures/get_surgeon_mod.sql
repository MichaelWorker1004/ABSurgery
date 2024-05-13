create proc get_surgeon_mod
  @candidate char(6),
  @status char (  1 )
as 
  select convert(varchar(12),modified) from surgeon
          where candidate = @candidate and status=@status  
