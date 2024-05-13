CREATE proc [dbo].[get_login_XML]
	@code as varchar(100),
	@pwd as varchar(100)=null,
	@ip as varchar(15)=null,
	@uri as varchar(100)=null,
	@brw as varchar(200)=null,
	@logtime as varchar(22)=null
as
	declare @candidate varchar(6)	
	SET NOCOUNT ON	

	if len(@code)>30 	begin
		set @code=(select candidate from appreflet where id_code=@code)
	end
	
	if @ip is not null
	begin
		if len(isnull(@pwd,''))!=0 		begin
			select @candidate=(	--select candidate from surgeon where pwd = @pwd and lower(uid)=lower(@code)
							--union
						select number+exam from program where pwd = @pwd and number+exam=@code)
		end
		else
		begin
			set @candidate=@code
		end
		insert weblog (candidate,ip,uri,brw,logtime) values (isnull(@candidate,'fail')+'-'+isnull(@code,''),@ip,@uri,@brw,getdate())
	end
	
	if len(isnull(@pwd,''))!=0 	begin
		select @candidate=(	--select candidate from surgeon where pwd = @pwd and lower(uid)=lower(@code)
						--union
					select number+exam from program where pwd = @pwd and number+exam=@code)
		
		if (len(@candidate)=6)
		begin
			select 
			isnull(pwd_reset,0) pwd_reset,
			board_unique_id as cand,
			isnull(surgeon_st.note,'') last_visited,
                        		isnull(surgeon_st.st_val,'') save_last_visited,
			info.ssn,[first name] as fname,[middle name] as mname,[last name] as lname,
			[first name]+' '+[middle name]+' '+[last name] as fullname,
			address1 as addr1,address2 as addr2,address3 as addr3,address4 as addr4,city,[state or province] as state,
			isnull(case info.country when 'USA' then [zip code] else [foreign zip code] end,'') as zip,info.country,
			StatusNbr,LastVerified=(case year(isnull(LastVerified,'')) when 1900 then '' else isnull(convert(varchar(10),LastVerified,101),'') end) ,
			replace(info.birthdate,'/','') as bdate,
			
			adm_exam,adm_type,adm_year_start,adm_year_end ,
			
			track.exam,track.type,track.examtype,track.year,
			case dbo.udf_get_app_eligibility_bin(board_unique_id,track.exam+track.type) when 'true' then track.exam+track.type else '' end eligible,
			app_rcvd=(case year(isnull(app_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),app_rcvd,101),'') end),
			app_appr=(case year(isnull(app_appr,'')) when 1900 then '' else isnull(convert(varchar(10),app_appr,101),'') end),
			appfee_rcvd=(case year(isnull(appfee_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),appfee_rcvd,101),'') end),
			rply_sent=(case year(isnull(rply_sent,'')) when 1900 then '' else isnull(convert(varchar(10),rply_sent,101),'') end),
			rply_rcvd=(case year(isnull(rply_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),rply_rcvd,101),'') end),
			adms_sent=(case year(isnull(adms_sent,'')) when 1900 then '' else isnull(convert(varchar(10),adms_sent,101),'') end),
			regfee_rcvd=(case year(isnull(regfee_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),regfee_rcvd,101),'') end),
			rtrim(isnull(pf,'')) as pf,
			isnull(rtrim(ltrim(area)),'') as area,isnull(day,'') as day,isnull(briefing,'') as briefing, asgn_area=(track.Descr),
			exam_start_date=isnull(attr,''),
			asgn_briefing= (case briefing when 1 then (case exam when 'G' then '  7:00AM' when 'P' then '  7:45AM' when 'V' then '  7:00AM' when 'O' then '  7:00AM' else ''  end) when 2 then (case exam when 'G' then ' 12:00 PM' when 'P' then ' 12:15 PM' when 'V' then ' 11:30 AM' when 'O' then '  1:00 PM' else '' end)  else '' 
				end) ,
			track.status,
			isnull(campaign,'') survey 
			from v_abms_address as info 
			
			left outer join 
				(select cand, null adm_exam,null adm_type,null adm_year_start,null adm_year_end,
					exam,type,examtype,year,app_rcvd,app_appr,appfee_rcvd,rply_sent,rply_rcvd,adms_sent,regfee_rcvd,pf,area,briefing,descr,attr,day,status 
				 from V_EXAM_TRACK track 
				 left outer join	mcodes on mcodes.grp = 'EC' and cast(track.year as varchar(4))+track.exam+'O'+cast(track.area as varchar(1)) = mcodes.code
				 where  track.year>2003 
					union
				select candidate cand,exam,type,cast(year_start as varchar),cast(year_end as varchar) ,
					null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null 
				from exam_eligibility info where admissible!='N'	
			) track on track.cand=info.board_unique_id
			inner join surgeon on surgeon.candidate=info.board_unique_id
                        		left join surgeon_st on surgeon_st.candidate=info.board_unique_id and status_code='LV' and st_val='yes'
			left join (select campaign,candidate from survey group by candidate,campaign) survey on survey.candidate=board_unique_id and survey.campaign=cast(track.year as varchar)+track.exam+track.examtype+track.area 
			where pwd = @pwd and lower(uid)=lower(@code)
			order by track.year,track.exam,track.type,area	
			for xml auto,elements
		end
		else
		begin
			select rtrim(number+info.exam) as cand,
			isnull(pwd_reset,0) pwd_reset,
			isnull(surgeon_st.note,'') last_visited,
                        		isnull(surgeon_st.st_val,'') save_last_visited,
                        		'' ssn,isnull(name1,'') as fname,'' as mname,isnull(name2,'') as lname,
			'' as fullname,
			'' addr1,'' addr2,'' addr3,'' addr4,'' city,'' state,
			'' zip,
			'' country,
			'' StatusNbr,'' LastVerified,
			'' as bdate,info.exam,'' type,'' year,
			'' eligible,
			'' app_rcvd,
			'' app_appr,
			'' appfee_rcvd,
			'' rply_sent,
			'' rply_rcvd,
			'' adms_sent,
			'' regfee_rcvd,
			'' pf,
			'' area,'' day,'' briefing, '' asgn_area,
			'' exam_start_date,
			'' asgn_briefing,'' status ,'' survey 
			from program as info 
                        		left join surgeon_st on rtrim(surgeon_st.candidate)=number+info.exam and status_code='LV' and st_val='yes'
			left outer join track track on track.exam='x'
			where pwd = @pwd and number+info.exam=@code for xml auto,elements
		end
	end
	else
	begin 		if (len(@code)=6)
		begin
			select 
			board_unique_id as cand,
			isnull(pwd_reset,0) pwd_reset,
			isnull(surgeon_st.note,'') last_visited,
                        		isnull(surgeon_st.st_val,'') save_last_visited,
			info.ssn,[first name] as fname,[middle name] as mname,[last name] as lname,
			[first name]+' '+[middle name]+' '+[last name] as fullname,
			address1 as addr1,address2 as addr2,address3 as addr3,address4 as addr4,city,[state or province] as state,
			isnull(case info.country when 'USA' then [zip code] else [foreign zip code] end,'') as zip,info.country,
			StatusNbr,LastVerified=(case year(isnull(LastVerified,'')) when 1900 then '' else isnull(convert(varchar(10),LastVerified,101),'') end) ,
			replace(info.birthdate,'/','') as bdate,
			
			adm_exam,adm_type,adm_year_start,adm_year_end ,
			
			track.exam,track.type,track.examtype,track.year,
			case dbo.udf_get_app_eligibility_bin(board_unique_id,track.exam+track.type) when 'true' then track.exam+track.type else '' end eligible,
			app_rcvd=(case year(isnull(app_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),app_rcvd,101),'') end),
			app_appr=(case year(isnull(app_appr,'')) when 1900 then '' else isnull(convert(varchar(10),app_appr,101),'') end),
			appfee_rcvd=(case year(isnull(appfee_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),appfee_rcvd,101),'') end),
			rply_sent=(case year(isnull(rply_sent,'')) when 1900 then '' else isnull(convert(varchar(10),rply_sent,101),'') end),
			rply_rcvd=(case year(isnull(rply_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),rply_rcvd,101),'') end),
			adms_sent=(case year(isnull(adms_sent,'')) when 1900 then '' else isnull(convert(varchar(10),adms_sent,101),'') end),
			regfee_rcvd=(case year(isnull(regfee_rcvd,'')) when 1900 then '' else isnull(convert(varchar(10),regfee_rcvd,101),'') end),
			rtrim(isnull(pf,'')) as pf,
			isnull(rtrim(ltrim(area)),'') as area,isnull(day,'') as day,isnull(briefing,'') as briefing, asgn_area=(track.Descr),
			exam_start_date=isnull(attr,''),
			asgn_briefing= (case briefing when 1 then (case exam when 'G' then '  7:00AM' when 'P' then '  7:45AM' when 'V' then '  7:00AM' when 'O' then '  7:00AM' else ''  end) when 2 then (case exam when 'G' then ' 12:00 PM' when 'P' then ' 12:15 PM' when 'V' then ' 11:30 AM' when 'O' then '  1:00 PM' else '' end)  else '' end) ,
			track.status,
			isnull(campaign,'') survey

			from v_abms_address as info 
			
			left outer join 
				(select cand, null adm_exam,null adm_type,null adm_year_start,null adm_year_end,
					exam,type,examtype,year,app_rcvd,app_appr,appfee_rcvd,rply_sent,rply_rcvd,adms_sent,regfee_rcvd,pf,area,briefing,descr,attr,day,status 
				 from V_EXAM_TRACK track 
				 left outer join	mcodes on mcodes.grp = 'EC' and cast(track.year as varchar(4))+track.exam+'O'+cast(track.area as varchar(1)) = mcodes.code
				 where  track.year>2003 
					union
				select candidate cand,exam,type,cast(year_start as varchar),cast(year_end as varchar) ,
					null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null 
				from exam_eligibility info where admissible!='N'	
			) track on track.cand=info.board_unique_id	
			inner join surgeon on surgeon.candidate=info.board_unique_id 
                        		left join surgeon_st on surgeon_st.candidate=info.board_unique_id  and status_code='LV' and st_val='yes'
			left join (select campaign,candidate from survey group by candidate,campaign) survey on survey.candidate=board_unique_id and survey.campaign=cast(track.year as varchar)+track.exam+track.examtype+track.area  
			where board_unique_id=@code
			order by track.year,track.exam,track.type,area	
			for xml auto,elements	
		end
		else
		begin
			select rtrim(number+exam) as cand,
			isnull(pwd_reset,0) pwd_reset,
			isnull(surgeon_st.note,'') last_visited,
                        		isnull(surgeon_st.st_val,'') save_last_visited,
                        		'' ssn,isnull(name1,'') as fname,'' as mname,isnull(name2,'') as lname,
			'' as fullname,
			'' addr1,'' addr2,'' addr3,'' addr4,'' city,'' state,
			'' zip,
			'' country,
			'' StatusNbr,'' LastVerified,
			'' as bdate,exam,'' type,'' year,
			'' eligible,
			'' app_rcvd,
			'' app_appr,
			'' appfee_rcvd,
			'' rply_sent,
			'' rply_rcvd,
			'' adms_sent,
			'' regfee_rcvd,
			'' pf,
			'' area,'' day,'' briefing, '' asgn_area,
			'' exam_start_date,
			'' asgn_briefing,'' status ,'' survey
			
			from program as info 
                        		left join surgeon_st on rtrim(surgeon_st.candidate)=number+exam and status_code='LV' and st_val='yes'
			where number+exam=@code for xml auto,elements
		end
	end