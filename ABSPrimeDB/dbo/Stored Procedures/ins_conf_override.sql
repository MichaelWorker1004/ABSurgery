--drop proc ins_conf_override
CREATE proc ins_conf_override
   @candidate 	char (6),
   @exam 	exam,
   @type 	char (1),
   @year 	smallint,
   @override    bit=null--,
   --@nt_username varchar(15)=null output
as 
	declare  @nt_username varchar(15)
	SET NOCOUNT ON
	
	if @override=1
	begin
		set @nt_username=null
	end
	else
	begin
		select @nt_username=nt_username from sysprocesses where spid = @@spid
		if len(isnull(@nt_username,''))=0
		begin
			set @nt_username=suser_sname()
		end
	end

	update exam set 
	        conf_override = rtrim(@nt_username)
	        where candidate = @candidate and
		exam = @exam and
	      	type = @type and
	      	year = @year

	select @nt_username
