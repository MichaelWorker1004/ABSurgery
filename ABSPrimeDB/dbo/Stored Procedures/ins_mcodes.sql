--drop proc ins_mcodes
CREATE proc ins_mcodes
	@grp		char(2),
	@code		varchar(25),
   	@descr	 	varchar(100),
   	@attr	 	varchar(50),
	@attr2		varchar(200)
as 
	SET NOCOUNT ON
	if len(@attr)=0 set @attr=null
	if len(@attr2)=0 set @attr2=null
	
	if exists(select * from mcodes where grp=@grp and code=@code)
	begin
		select 'Record for grp='+@grp+' and code='+@code+' already exists!'
	end
	else
	begin
		insert mcodes (grp,code,descr,attr,attr2) 
			values (@grp,@code,@descr,@attr,@attr2)
		select 'Record has been added succesfully!'
	end
