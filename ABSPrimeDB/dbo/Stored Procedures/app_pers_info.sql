CREATE proc [dbo].[app_pers_info]
	@address_c	bit=0,
	@phone_c	bit=0,
	@fax_c		bit=0,
	@email_c	bit=0,
	@surgeon_c      bit=0,

	@code char (6) ,
	@street1 varchar (100)  ,
	@street2 varchar (100)  ,
	@city varchar (30)  ,
	@state char (2)  ,
	@zip char (10)  ,
	@country varchar (60)  ,

	@phone varchar(100),
	@fax varchar(100),
	@email varchar(100),

	@birthcountry varchar(60),
	@birthdate varchar(10),
	@gender varchar(6),
	@race varchar(20),
	@ethnicity varchar(20),
	
	@dispname varchar(100),
	@practicedesc varchar(8000),

	@schoolcountry varchar (60),
	@schooltype varchar (1),
	@degree varchar (20),
	@year smallint=null, 

	@uid varchar(12),
	@pwd varchar(100),

	@npi varchar(10)=null


as  
	DECLARE @address_type char(1),@phone_type  char(1),@iMSG tinyint
	
	SET NOCOUNT ON
	set @address_type=null
	set @phone_type=null
	set @iMSG=0

	if (@address_c=1)
	begin
		set @address_type=(select type from address where code = @code and status='S' and mail='M')		
		if @address_type is not null
		begin
			update address set
				title           = ''		,
				institution     = ''		,
				street1         = @street1 	,
				street2         = @street2	,
				street3		= ''		,
				street4         = @country 	,
				city            = @city		,
				state           = @state	,
				zip             = @zip		,
				modified        = getdate()   
			where code = @code and  type = @address_type and status = 'S' and mail='M'			
		end
		else
		begin
			set @address_type='O'			
			if exists(select type from address where code = @code and status='S' and type='O' )
			begin
					update address set
						title           = ''		,
						institution     = ''		,
						street1         = @street1 	,
						street2         = @street2	,
						street3		= ''		,
						street4         = @country 	,
						city            = @city		,
						state           = @state	,
						zip             = @zip		,
						mail		= 'M'		,
						modified        = getdate()   
					where code = @code and  type = 'O' and status = 'S'
			end
			else
			begin
					insert address ( type, code, mail, title, institution,
						status, street1, street2, street4,
						city, state, zip, modified)
					values   ( 'O', @code, 'M', '', '',
						'S', @street1, @street2, @country,
						@city, @state, @zip, getdate())
			end
		end
	end
	
	if (@phone_c=1)
	begin
		if @address_type is null
		begin
			set @phone_type=(select type from address where code = @code and status='S' and mail='M')
			set @address_type=@phone_type
		end
		else
		begin
			set @phone_type=@address_type
		end
		
		if @phone_type is null
		begin
			set @phone_type=(select top 1 type1 from phone where code = @code and status='S' and type2='V')
		end
		
		if @phone_type is null
		begin
			set @phone_type='O'
		end

		if exists(select type1 from phone where code = @code and status='S' and type2='V' and type1=@phone_type)
		begin
			update phone set number= @phone,modified=getdate() 
			where code = @code and  type1 = @phone_type and status = 'S' and type2='V'			

		end
		else
		begin
			insert phone (code,number,type1,type2,status,modified)
			values (@code,@phone,@phone_type,'V','S',getdate())
		end
	end

	if (@fax_c=1)
	begin
		if @address_type is null
		begin
			set @phone_type=(select type from address where code = @code and status='S' and mail='M')
			set @address_type=@phone_type
		end
		else
		begin
			set @phone_type=@address_type
		end

		if @phone_type is null
		begin
			set @phone_type=(select top 1 type1 from phone where code = @code and status='S' and type2='F')
		end
		
		if @phone_type is null
		begin
			set @phone_type='O'
		end

		if exists(select type1 from phone where code = @code and status='S' and type2='F' and type1=@phone_type)
		begin
			update phone set number= @fax,modified=getdate() 
			where code = @code and  type1 = @phone_type and status = 'S' and type2='F'			

		end
		else
		begin
			insert phone (code,number,type1,type2,status,modified)
			values (@code,@fax,@phone_type,'F','S',getdate())
		end
	end

	if (@email_c=1)
	begin
		if @address_type is null
		begin
			set @phone_type=(select type from address where code = @code and status='S' and mail='M')
			set @address_type=@phone_type
		end
		else
		begin
			set @phone_type=@address_type
		end

		if @phone_type is null
		begin
			set @phone_type=(select top 1 type1 from phone where code = @code and status='S' and type2='B')
		end
		
		if @phone_type is null
		begin
			set @phone_type='O'
		end

		if exists(select type1 from phone where code = @code and status='S' and type2='B' and type1=@phone_type)
		begin
			update phone set number= @email,modified=getdate() 

			where code = @code and  type1 = @phone_type and status = 'S' and type2='B'			

		end
		else
		begin
			insert phone (code,number,type1,type2,status,modified)
			values (@code,@email,@phone_type,'B','S',getdate())
		end
	end


	if (@surgeon_c=1)
	begin
		select @birthcountry=code from country where description=@birthcountry
		select @schoolcountry=code from country where description=@schoolcountry
		select @degree=type from degree where description=@degree
		if isnull(@pwd,'')=''
		begin		
			update surgeon set
				birthplace          	= isnull(@birthcountry,''),
				birthdate     	= IIF(LEN(RTRIM(@birthdate))=0,NULL,cast(@birthdate as datetime)),
				sex         	= upper(left(@gender,1)),
				race         	= isnull(@race,''),
				ethnicity      	= isnull(@ethnicity,''),
				modified		= getdate() ,
				dispname	= @dispname,
				practicedesc	=@practicedesc,
				country         	= isnull(@schoolcountry,''),
				school			=isnull(@schooltype,''),
				degree     	= @degree,
				year         	= @year,
				npi		= @npi
			where candidate = @code
		end
		else if exists(select uid from surgeon where lower(uid)=lower(@uid) and candidate != @code)
		begin
			set @iMSG=3
		end
		else if (@uid=@code)
		begin
			set @iMSG=21
		end
		else
		begin
			/*if not exists(select uid from surgeon where candidate=@code and pwd = @pwd)--password modified
			begin
				update surgeon set
					pwd		= @pwd,
					pwd_last_modified= getdate() 
				where candidate = @code				
			end				
			*/
			update surgeon set
				birthplace          	= isnull(@birthcountry,''),
				birthdate     	= IIF(LEN(RTRIM(@birthdate))=0,NULL,cast(@birthdate as datetime)),
				sex         	= upper(left(@gender,1)),
				race         	= isnull(@race,''),
				ethnicity      	= isnull(@ethnicity,''),
				uid		= @uid,
				pwd		= @pwd,
				pwd_last_modified= getdate(),
				pwd_reset = 0,					
				modified		= getdate() ,
				dispname	= @dispname,
				practicedesc	=@practicedesc,
				country         	= isnull(@schoolcountry,''),
				school			=isnull(@schooltype,''),
				degree     	= @degree,
				year         	= @year,
				npi		=@npi
			where candidate = @code
		end
	end
	
	if @iMSG!=0 
	begin
		select attr2 msg from mcodes info where grp='MG' and code=@iMSG
	end