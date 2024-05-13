CREATE proc get_address
  @candidate char(6),
  @type char ( 1 ),
  @status char ( 1 )
as  

   select  title, institution, type, street1, street2, 
        street4,  city, state, zip, note,  convert(varchar(12),modified), location, mail
        from address        
        where code = @candidate and type=@type and status=@status
