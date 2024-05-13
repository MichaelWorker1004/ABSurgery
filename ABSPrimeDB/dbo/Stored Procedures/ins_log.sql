CREATE proc ins_log
	@activity varchar ( 60 ),@log_data text=null,@modifier varchar(30)=null
as 
	declare  @logtime char(23),@nt_username varchar(15)

	select @nt_username=nt_username from sysprocesses where spid = @@spid
	
	select @logtime = convert(varchar(30),getdate(),121)
	
	if @modifier is null
	begin
		set @modifier=user_name()
	end

	insert abslogs(logtime,absuser,activity,nt_username,suser_sname,app_name,log_data) 
		values(@logtime,@modifier,@activity,@nt_username,suser_sname(),APP_NAME ( ) ,@log_data)