

CREATE proc ins_address
   @type char (  1 ) , /*O,H,A,V -off,hom,alt,vac*/
   @status char (  1 ) , /*S,A,P =Surg,Applic,Prog*/
   @code char (  6 ) ,
   @location char (  1 ) ,
   @mail char (  1 ) ,
   @title varchar (  50 )  ,
   @institution varchar (  50 )  ,
   @street1 varchar (  100 )  ,
   @street2 varchar (  100 )  ,
   @street4 varchar (  50 )  ,
   @city varchar (  30 )  ,
   @state char (  2 )  ,
   @zip char (  10 )  ,
   @note varchar (  255 ) , 
   @fedex bit,
   @modified modified=null
as  
   
   if exists (select code from address
              where code = @code and
              type = @type and
              status = @status  )
   begin
       update address set
              location        = @location    ,
              status          = @status      ,
              mail            = @mail        ,
              title           = @title       ,
              institution     = @institution ,
              street1         = @street1     ,
              street2         = @street2     ,
              street4         = @street4     ,
              city            = @city        ,
              state           = @state       ,
              zip             = @zip         ,
              note            = @note        ,
	 fedex		=@fedex,
              modified        = getdate()  
       where code = @code and  type = @type and status = @status
   end
   else
   begin          
           insert address ( type, code, location, mail, title, institution,
                    status, street1, street2, street4,
                    city, state, zip, fedex, note, modified)
         values   ( @type, @code, @location, @mail, @title, @institution,
                    @status, @street1, @street2, @street4,
                    @city, @state, @zip, @fedex, @note, @modified)
   end