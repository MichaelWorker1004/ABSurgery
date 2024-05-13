create proc get_phone
  @candidate char(6),
  @type1 char ( 1 ),
  @status char ( 1 )
as  
   select type2, number from phone 
        where code = @candidate and type1=@type1 and status=@status
