CREATE proc [dbo].[ins_app_response_XML]
	@candidate as char(6),
	@app_type as varchar(8),
	@app_id as tinyint,
	@version  as tinyint,
	@response as text,
	@last_updt smalldatetime,
	@status as tinyint=0

as
	DECLARE @year smallint
	SET NOCOUNT ON
	if @app_id is not null
	begin
		if (@app_id in (20,21) and right(@app_type,2)='RR') 			
		begin
				if (@status=1)
				begin
				DECLARE @old_rosterid numeric,@new_rosterid numeric
				
				select @year=left(@app_type,4)
	
				select @old_rosterid=id from roster where 
						rtrim(number+exam)=rtrim(@candidate) and 
						current_year=@year
				
				if @app_id=21
				begin
					update roster set finalized=getdate() where id=@old_rosterid				
				end	
				else if (right(rtrim(@candidate),1)='G')
				begin
					update roster set verified=getdate() where id=@old_rosterid
					if not exists 
						(select z.id from roster z
							inner join roster a on a.number=z.number and a.exam=z.exam and z.current_year=a.current_year+1 and a.id=@old_rosterid)
					begin
						insert 	roster 
							(created, creator,exam,current_year,number,pd_auth) 
						select 	getdate(),'sp', roster.exam,current_year+1,roster.number,isnull(pd,'') 
							from roster
							inner join program on program.number+program.exam=roster.number+roster.exam
							where roster.id=@old_rosterid
						
						set @new_rosterid=@@identity
			
						insert 	resident 
							(created, creator,last_name,first_name,middle_name,ssn,level,research,comp_year,candidate,roster_id,notes) 
						select 	getdate(),'sp', last_name,first_name,middle_name,ssn,
							case 
								when level<4 then level+1
								when level=5 then 9
								else level
							end,
							research,comp_year,candidate,@new_rosterid,
							case
								when level=5 then isnull(notes,'')+' (Residency Level 5 in '+cast(@year as varchar)+')'
								else notes
							end
						from resident where 
							roster_id=@old_rosterid and comp_year>=@year+1
					end
				end
			end		
			select @response response,@status
		end			
		else 
		begin
			if (@app_id not in (5,7,8,9,11,17,18,20,23,24,31,32,33,35,36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,52,53,54,55,56,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72)  and CHARINDEX ( 'app_id="'+cast(@app_id as varchar) ,@response) >0 and (CHARINDEX ( 'app_type="'+@app_type ,@response) >0 or @app_id=15))
			begin
				if (@app_id=15)				
				begin
					set @app_type='RCD'
				end
				
				if exists(select app_id from app_response where app_id=@app_id and candidate=@candidate and exam_type=@app_type and version=@version)
				begin
					update app_response set response=@response,status=@status,last_updt=@last_updt where app_id=@app_id and candidate=@candidate and exam_type=@app_type and version = @version
				end
				else
				begin
					insert into app_response (candidate,exam_type,app_id,version,response,status,last_updt) values (@candidate,@app_type,@app_id,@version,@response,@status,@last_updt)
				end
				
				if ((len(rtrim(@app_type))>5 OR ((CHARINDEX('R',@app_type)>0 OR CHARINDEX('X',@app_type)>0) AND @app_id=1)) and right(@app_type,3)!='SCR' and right(@app_type,3)!='ITE' and @app_id!=57 and @app_id!=63)
				begin
					if not exists(select candidate from track where candidate=@candidate and cast(year as varchar)+exam+type=@app_type) AND LEN(@app_type)>2
						AND CHARINDEX('MC',@app_type)=0 					begin
						insert into track (creator,trackno,candidate,exam,type,year) 
							values ('web',@candidate,@candidate,substring(@app_type,5,1),substring(@app_type,6,1),left(@app_type,4))
						insert into abslogs(logtime,app_name,absuser,activity)
							values 	(convert(varchar(20),getdate(),2) + ' ' +  convert(varchar(20),getdate(),8),'web','web',@candidate+' Track record created for '+@app_type)
					end
				
					if (@status=1)					
					begin
						if (@app_id=0) 						
						begin
							update track set app_receive=@last_updt,modifier='web',modified=getdate(),modifications=modifications+1 where candidate=@candidate and cast(year as varchar)+exam+type=@app_type
							/*if (CHARINDEX('MC',@app_type)>0)
							begin
								update cme set usedwith=@app_type where candidate=@candidate and isnull(usedwith,'') in ('','true');
								update cme set usedwith='true' where candidate=@candidate and isnull(usedwith,'') = 'false';
								update moc_eligibility set num_cycles_non_compliant=0 where candidate=@candidate;
							end*/

							if (CHARINDEX('GQ',@app_type)>0 or CHARINDEX('VQ',@app_type)>0)
							begin
								EXEC ins_education_XML @candidate, @app_type, @response;
							end		
						end
						else if (@app_id=1 AND 	(
													(
														(SELECT opproc FROM dbo.udf_get_opex(@candidate,@app_type))>0 AND CHARINDEX('R',@app_type)>0
													) 
													OR CHARINDEX('CR',@app_type)>0
													OR CHARINDEX('ZR',@app_type)>0
												)
								) 						
						begin
							IF LEN(@app_type)>2
							BEGIN
								update track set oper_receive=@last_updt,oper_approve=@last_updt,modifier='web',modified=getdate(),modifications=modifications+1 where candidate=@candidate and cast(year as varchar)+exam+type=@app_type							
							END
							ELSE
							BEGIN
								update app_response set approved=@last_updt where app_id=@app_id and candidate=@candidate and exam_type=@app_type and version = @version
							END						
						end						
						else if (@app_id=1) 						
						begin
							IF LEN(@app_type)>2
							BEGIN
								update track set oper_receive=@last_updt,modifier='web',modified=getdate(),modifications=modifications+1 where candidate=@candidate and cast(year as varchar)+exam+type=@app_type
							END
							ELSE IF (LEN(@app_type)=2 and (CHARINDEX('GX', @app_type)>0 or CHARINDEX('VX', @app_type)>0))
							BEGIN
								update track set oper_receive=@last_updt,modifier='web',modified=getdate(),modifications=modifications+1 where candidate=@candidate and cast(year as varchar)+exam+type=cast(dbo.udf_get_academic_year('T') as varchar)+@app_type
							END
							
							IF CHARINDEX('R',@app_type)>0 							BEGIN
				    			IF EXISTS (SELECT id FROM surgeon_st WHERE candidate=@candidate AND status_code='CA'+@app_type) 
				    			BEGIN
				    				UPDATE surgeon_st SET modified=getdate(), modifications=modifications+1 ,modifier='iar',
				    					note = ISNULL(note,'')+'1,'
				    					WHERE candidate=@candidate AND status_code='CA'+@app_type AND isnull(note,'') not like '%1,%'
				    			END 
				    			ELSE 
				    			BEGIN 
				    				INSERT INTO surgeon_st (creator,created,modifications,candidate,status_code,note) VALUES ('iar',getdate(),0,@candidate,'CA'+@app_type,'1,'); 
				    			END 							
							END
						end
						else if (@app_id=25) 						
						begin
							update track set surv_receive=@last_updt,modifier='web',modified=getdate(),modifications=modifications+1 where candidate=@candidate and cast(year as varchar)+exam+type=@app_type
						end
					end
				end
				ELSE
				IF right(@app_type,3)='ITE' AND @status=1 AND @app_id=6 				BEGIN
					UPDATE
					    resident
					SET
						resident.orderformclinicallevel=v_resident.orderformclinicallevel
					FROM
					    resident
					    INNER JOIN v_resident ON resident.id = v_resident.id
					    INNER JOIN roster ON roster.id=resident.roster_id
					WHERE
					    cast(roster.current_year AS VARCHAR(4))=LEFT(@app_type,4) AND roster.number+roster.exam=@candidate
				END
				ELSE
				IF @app_id=57 and @status=1
				BEGIN
					UPDATE track SET fpd_receive=getdate() where candidate=@candidate and examcode=@app_type
				END			
			end
			if (@app_id not in (5,11,17,18,23,24,31,33,35,36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,52,53,54,55,56,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72))			
			begin
				exec get_app_response_XML @candidate,@app_type,@app_id,@version
			end
			else
			begin
				select @response response
			end
			
		end
	end