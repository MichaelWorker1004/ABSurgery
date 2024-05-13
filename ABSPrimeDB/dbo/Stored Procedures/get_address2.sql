CREATE proc get_address2
  @candidate char(6),
  @mail char ( 1 ),
  @status char ( 1 )
as  

   select  title, institution,  type, street1, street2, 
        street4,  city, state, zip, note,  convert(varchar(12),modified), location, mail
        from address        
        where code = @candidate and mail=@mail and status=@status
