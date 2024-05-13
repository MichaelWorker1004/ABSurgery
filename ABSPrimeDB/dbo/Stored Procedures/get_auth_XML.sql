CREATE proc [dbo].[get_auth_XML]
	@uid as varchar(12),
	@pwd as varchar(100),	
	@cand as varchar(6),
	@bday as varchar(11),
	@ssn as varchar(9),
	@cert as varchar(6),	
	@program as varchar(5),
	@email as varchar(100),	
	
	@ip as varchar(15)=null,
	@uri as varchar(100)=null,
	@brw as varchar(200)=null,
	@logtime as varchar(22)=null
as
	declare @sUID as varchar(12),@sUID_EXISTS as varchar(12),@sCand as varchar(6),@iMSG as tinyint,@sAddressPrimeType as char(1),@sEmailPrimeType as char(1),@sEmailOfficeType as char(1)
	SET NOCOUNT ON	
	if @ip is not null
	begin
		insert weblog (candidate,ip,uri,brw,logtime) values (@cand,@ip,@uri,@brw,getdate())
	end

	if @cand='' begin set @cand='%' end
	if @bday='' begin set @bday='%' end
	if @ssn='' begin set @ssn='%' end
	if @cert='' begin set @cert='%' end

	select @sUID=uid,@sCand=candidate,@sUID_EXISTS=(select uid from surgeon where lower(uid)=lower(@uid) and len(rtrim(@uid))<>0 union select number+exam from program where number+exam=@uid and len(rtrim(@uid))<>0),@sAddressPrimeType=a.type,@sEmailPrimeType=b.type1,@sEmailOfficeType=c.type1 
		from surgeon 
		left outer join address a on a.code=candidate and a.mail='M' and a.status='S'
		left outer join phone b on b.code=candidate and b.status='S' and b.type1=a.type and b.type2='B'
		left outer join phone c on c.code=candidate and c.status='S' and c.type1='O' and c.type2='B'
		where candidate like @cand and isnull(ssn,'') like @ssn 
			 and convert(varchar(11),isnull(birthdate,''),20) like @bday 

		
	set @iMSG=0
	
	if @sCand is null
	begin
		set @iMSG=1	
	end
	else
	begin
		if not exists(select exam from candidate_program where candidate=@sCand and program+exam like @program+'%') and @program<>''
		begin		
			set @iMSG=1
		end
		else
		begin	
			if not exists(select exam from exam_pass where candidate=@sCand and certificate like @cert) and @cert<>'%'
			begin		
				set @iMSG=1
			end
			else
			begin		
				if isnull(@sUID,'')<>'' and rtrim(len(@uid))<>0
				begin
					set @iMSG=2	
				end
				else
				begin
					if @sUID_EXISTS is not null
					begin
						set @iMSG=3	
					end
					else
					begin
						if rtrim(len(@uid))<>0
						begin
							update surgeon set uid=@uid,pwd=@pwd,pwd_reset=0,pwd_last_modified=getdate() where candidate=@sCand
						end
						
						if @sAddressPrimeType is null
						begin
							if @sEmailOfficeType is null	
							begin
								insert into phone (creator,code,  status,type1,type2,number)
									values    ('web'  ,@sCand,'S'   ,'O'  ,'B'  ,@email)
	
							end
							else
							begin
								update phone set number=@email from phone where phone.code=@sCand and phone.status='S' and phone.type1='O' and phone.type2='B'
							end			
						end
						else
						begin
							if @sEmailPrimeType is null	
							begin
								insert into phone (creator,code,  status,type1               ,type2,number)
									values    ('web'  ,@sCand,'S'   ,@sAddressPrimeType  ,'B'  ,@email)
							end
							else
							begin
								update phone set number=@email from phone where phone.code=@sCand and phone.status='S' and phone.type1=@sAddressPrimeType and phone.type2='B'
							end						
						end
					end
				end
			end
		end
	end

	if @iMSG=0 
	begin
		exec get_login_XML @sCand,'',@ip,@uri,@brw,@logtime
	end
	else
	begin
		select attr2 msg from mcodes info where grp='MG' and code=@iMSG for xml auto,elements
	end