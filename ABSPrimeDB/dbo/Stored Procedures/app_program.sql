CREATE proc app_program
	@address_c	bit=0,
	@phone_c	bit=0,
	@fax_c		bit=0,
	@email_c	bit=0,

	@code varchar (5) ,
	@type char(1),
	@institution varchar(50),
	@street1 varchar (  100 )  ,
	@street2 varchar (  100 )  ,
	@city varchar (  30 )  ,
	@state char (  2 )  ,
	@zip char (  10 )  ,

	@phone varchar(100),
	@fax varchar(100),
	@email varchar(100),
	@country varchar ( 50 )

as  
	DECLARE @mail char(1)
	SET NOCOUNT ON

	if (@address_c=1)
	begin
		if exists(select type from address where code = @code and status='P' and type=@type)			
		begin
			update address set
				title           = ''		,
				institution     = @institution		,
				street1         = @street1 	,
				street2         = @street2	,
				street3		= ''		,
				city            = @city		,
				state           = @state	,
				zip             = @zip		,
				modified        = getdate(),
				street4         = @country
			where code = @code and status = 'P' and type=@type		
		end
		else
		begin
			if exists(select type from address where code = @code and status='P' and mail='M')	
			begin
				set @mail=null
			end 
			else
			begin
				set @mail='M'
			end
			
			insert address ( mail,type, code, title, institution,
				status, street1, street2, 
				city, state, zip, modified, street4)
			values   (@mail,@type, @code, '', @institution,
				'P', @street1, @street2, 
				@city, @state, @zip, getdate(), @country)
		end
	end
	
	if (@phone_c=1)
	begin
		if exists(select type1 from phone where code = @code and status='P' and type2='V' and type1=@type)
		begin
			update phone set number= @phone,modified=getdate() 
			where code = @code and  type1 = @type and status = 'P' and type2='V'			

		end
		else
		begin
			insert phone (code,number,type1,type2,status,modified)
			values (@code,@phone,@type,'V','P',getdate())
		end
	end

	if (@email_c=1)
	begin
		if exists(select type1 from phone where code = @code and status='P' and type2='B' and type1=@type)
		begin
			update phone set number= @email,modified=getdate() 
			where code = @code and  type1 = @type and status = 'P' and type2='B'			

		end
		else
		begin
			insert phone (code,number,type1,type2,status,modified)
			values (@code,@email,@type,'B','P',getdate())
		end
	end

	if (@fax_c=1)
	begin
		if exists(select type1 from phone where code = @code and status='P' and type2='F' and type1=@type)
		begin
			update phone set number= @fax,modified=getdate() 
			where code = @code and  type1 = @type and status = 'P' and type2='F'			

		end
		else
		begin
			insert phone (code,number,type1,type2,status,modified)
			values (@code,@fax,@type,'F','P',getdate())
		end
	end
	select @@error