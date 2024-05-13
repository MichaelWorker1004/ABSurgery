--DROP PROC app_gqe_app
CREATE proc app_gqe_app
		@code char (  6 ) ,
		@country varchar (  40 )  ,
		@degree varchar (20)	 ,
		@dispname varchar(100),
		@year smallint=null 
		

as  
	SET NOCOUNT ON
	begin
		select @country=code from country where description=@country
		select @degree=type from degree where description=@degree
		begin
			update surgeon set
				country          	= isnull(@country,''),
				degree     	= @degree,
				year         	= @year,
				dispname	=@dispname,
				modified		= getdate() 
			where candidate = @code
		end
	end
