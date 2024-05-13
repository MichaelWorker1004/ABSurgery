

CREATE proc ins_phone
   @type1 char (  1 ) ,
   @status char (  1 ) ,
   @code char (  6 ) ,
   @number_voice varchar (  30 ),
   @number_FAX varchar (  30 ),
   @number_Beeper varchar (  100 ),
   @number_mobile varchar (  30 ),
   @modified modified=null
as  
   if exists (select code from phone where code=@code and type1=@type1 and type2='V' and status=@status) 
   begin
       update phone set
             number         = @number_voice,
             modified       = getdate()
       where code=@code and  type1=@type1 and type2='V' and status=@status
   end
   else
   begin  
	if len(ltrim(rtrim(@number_voice)))>0
	begin        
	       insert phone ( code, status, type1, type2, number, modified) values   ( @code, @status, @type1, 'V', @number_voice, @modified)
	end
   end

   if exists (select code from phone
      where code=@code and type1=@type1 and type2='F' and status=@status)
   begin
       update phone set
              number         = @number_FAX,
              modified       = getdate()
       where code=@code and  type1=@type1 and type2='F' and status=@status
   end
   else
   begin          
	if len(ltrim(rtrim(@number_FAX)))>0
	begin        
       		insert phone ( code, status, type1, type2, number, modified) values   ( @code, @status, @type1, 'F', @number_FAX, @modified)
	end
   end

   if exists (select code from phone
      where code=@code and type1=@type1 and type2='B' and status=@status)
   begin
       update phone set
              number         = @number_Beeper,
              modified       = getdate()
       where code=@code and  type1=@type1 and type2='B' and status=@status
   end
   else
   begin    
	if len(ltrim(rtrim(@number_Beeper)))>0
	begin        
        	insert phone ( code, status, type1, type2, number, modified) values   ( @code, @status, @type1, 'B', @number_Beeper, @modified)
	end
   end

   if exists (select code from phone
      where code=@code and type1=@type1 and type2='M' and status=@status)
   begin
       update phone set
              number         = @number_mobile,
              modified       = getdate()
       where code=@code and  type1=@type1 and type2='M' and status=@status
   end
   else
   begin          
	if len(ltrim(rtrim(@number_mobile)))>0
	begin        
       		insert phone ( code, status, type1, type2, number, modified) values   ( @code, @status, @type1, 'M', @number_mobile, @modified)
	end
   end