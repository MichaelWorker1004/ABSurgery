CREATE proc app_ref_let
		@modifier	char(3),
		@candidate	varchar(100),
		@exam_type	varchar(8),
		@let_type	tinyint,
		@sec_order tinyint,
		@id_code varchar(100),
		
		@let_rcvd	varchar(16),
		@coordinator_rcvd	varchar(16),
		@let_sent	varchar(16),

		@official varchar(85)=null,
		@coordinator varchar(85)=null,
		@explain varchar(8000)=null,
		@alternaterole varchar(200)=null,
		
		@title varchar(50)=null,
		@address varchar(200)=null,
		@city varchar(30)=null,
		@state varchar(2)=null,
		@zip varchar(10)=null,
		@email varchar(100)=null,
		@coordinator_email varchar(100)=null,
		@phone varchar(100)=null,
		@hosp varchar(100)=null,

		@sameinstitution varchar(5)=null,
		@fullprivileges varchar(5)=null,
		@professionalinteraction varchar(5)=null,
		@personalrelationship varchar(5)=null,
		@acceptableskills varchar(5)=null,
		@dependable varchar(5)=null,
		@communicationskills varchar(5)=null,
		@behavior varchar(5)=null,
		@participatesactivities varchar(5)=null,
		@substanceabuse varchar(5)=null,
		@disciplinaryactions varchar(5)=null,
		@scope varchar(5)=null,
		@recommend varchar(5)=null,
		
		@pd_completedres varchar(5)=null,
		@pd_sixoperative varchar(5)=null,
		@pd_sixclinical varchar(5)=null,
		@pd_endoscopy varchar(5)=null,	

		@pd_chiefweeks varchar(5)=null,
		@pd_pg4chieflet varchar(5)=null,
		@pd_reqhours varchar(5)=null,
		@pd_extension varchar(5)=null,	
		@pd_flexrots varchar(5)=null,
		@pd_introts varchar(5)=null,
		@pd_research varchar(5)=null,
		@pd_reqweeks varchar(5)=null,	
		@pd_documents varchar(5)=null,
		
		@remarks varchar(8000)=null,
		@signature varchar(5)=null,
		@coordinator_signature varchar(5)=null,
		@pd_confirm varchar(5)=null,
		@coordinator_confirm varchar(5)=null,
		@response_ip varchar(15)=null
as  
	begin
		DECLARE @id numeric

		if exists (select candidate from AppRefLet where 					
					(candidate=@candidate and exam_type=@exam_type and 
						(
							(sec_order=@sec_order AND @sec_order IS NOT NULL) 
								OR 
							(let_type=@let_type AND @sec_order IS NULL)
						)
					)
					OR 
					(
						id_code=@candidate AND @exam_type=''
					)
				)
		begin
			if @let_rcvd is null or (CHARINDEX('B', @exam_type)>0 and @coordinator_rcvd is null)
			begin
				update AppRefLet set
					let_rcvd=case 
						 when @signature='true' then getdate() 
						 when isnull(@let_sent,'')!='' then null
						 else let_rcvd end,
					coordinator_rcvd=case 
						 when @coordinator_signature='true' then getdate() 
						 when isnull(@let_sent,'')!='' then null
						 else coordinator_rcvd end,						 
					let_approve=case 
						 when @signature='true' or @coordinator_signature='true' or isnull(@let_sent,'')!='' then null
						 else let_approve end,						 
					let_sent=case isnull(@let_sent,'') when '' then let_sent else @let_sent end,
					
					alternaterole=@alternaterole,
					explain=@explain,
					official=@official,
					coordinator=@coordinator,
					title=@title,
					address=@address,
					city=@city,
					state=@state,
					zip=@zip,
					email=@email,
					coordinator_email=@coordinator_email,
					phone=@phone,
					hosp=@hosp,

					sameinstitution=@sameinstitution,
					fullprivileges=@fullprivileges,
					professionalinteraction=@professionalinteraction,
					personalrelationship=@personalrelationship,
					acceptableskills=@acceptableskills,
					dependable=@dependable,
					communicationskills=@communicationskills,
					behavior=@behavior,
					participatesactivities=@participatesactivities,
					substanceabuse=@substanceabuse,
					disciplinaryactions=@disciplinaryactions,
					scope=@scope,
					recommend=@recommend,
					
					pd_completedres=@pd_completedres,
					pd_sixoperative=@pd_sixoperative,
					pd_sixclinical=@pd_sixclinical,
					pd_endoscopy=@pd_endoscopy,		


					pd_chiefweeks=@pd_chiefweeks,
					pd_pg4chieflet=@pd_pg4chieflet,
					pd_reqhours=@pd_reqhours,
					pd_extension=@pd_extension,	
					pd_flexrots=@pd_flexrots,
					pd_introts=@pd_introts,
					pd_research=@pd_research,
					pd_reqweeks=@pd_reqweeks,	
					pd_documents=@pd_documents,			
					
					remarks=@remarks,
					signature=@signature,
					coordinator_signature=@coordinator_signature,
					pd_confirm=@pd_confirm,
					coordinator_confirm=@coordinator_confirm,
					response_ip=case @signature when 'true' then @response_ip else '' end,
										
					let_type=@let_type,
					modified=getdate(),
					modifier=@modifier,
					modifications=modifications+1
				where 
					(candidate=@candidate and exam_type=@exam_type and 
						(
							(sec_order=@sec_order AND @sec_order IS NOT NULL) 
								OR 
							(let_type=@let_type AND @sec_order IS NULL)
						)
					)
					OR 
					(
						id_code=@candidate AND @exam_type=''
					);

				select @id=id from appreflet where 
					(candidate=@candidate and exam_type=@exam_type and 
						(
							(sec_order=@sec_order AND @sec_order IS NOT NULL) 
								OR 
							(let_type=@let_type AND @sec_order IS NULL)
						)
					)
					OR 
					(
						id_code=@candidate AND @exam_type=''
					);				
					
			end
			else
			begin
				update AppRefLet set
					let_rcvd=case ltrim(rtrim(@let_rcvd)) when '' then null else @let_rcvd end,
					coordinator_rcvd=case ltrim(rtrim(@coordinator_rcvd)) when '' then null else @coordinator_rcvd end,
					signature=case ltrim(rtrim(@let_rcvd)) when '' then 'false' else signature end,
					coordinator_signature=case ltrim(rtrim(@coordinator_rcvd)) when '' then 'false' else coordinator_signature end,
					pd_confirm=ltrim(rtrim(@pd_confirm)),
					coordinator_confirm=ltrim(rtrim(@coordinator_confirm)),				
					response_ip=case ltrim(rtrim(@let_rcvd)) when '' then '' else response_ip end,
					let_sent=@let_sent,
					official=ltrim(rtrim(@official)),
					coordinator=ltrim(rtrim(@coordinator)),
					let_type=@let_type,
					modified=getdate(),
					modifier=@modifier,
					modifications=modifications+1					
				where candidate=@candidate and exam_type=@exam_type and sec_order=@sec_order
			end
		end
		else
		begin
			if @let_rcvd is null or (CHARINDEX('B', @exam_type)>0 and @coordinator_rcvd is null)
			begin		
				insert into AppRefLet
					(candidate,
					exam_type,
					let_type,
					let_sent,
					explain,	
					alternaterole,
					official,
					coordinator,
					title,
					address,
					city,
					state,
					zip,
					email,
					coordinator_email,
					phone,
					hosp,
				
					sec_order	
					)
				values
					(@candidate,
					@exam_type,
					@let_type,
					@let_sent,
					@alternaterole,
					@explain,				
					@official,
					@coordinator,
					@title,
					@address,
					@city,
					@state,
					@zip,
					@email,
					@coordinator_email,
					@phone,
					@hosp,
					
					@sec_order
					);
					
				select @id=@@identity;
			end
			else
			begin
				if (len(ltrim(rtrim(@official)))>0 or len(ltrim(rtrim(@coordinator)))>0 or len(ltrim(rtrim(@let_rcvd)))>0 or len(ltrim(rtrim(@coordinator_rcvd)))>0)
				begin
					insert into AppRefLet
						(candidate,
						exam_type,
						let_type,				
						official,
						coordinator,
						let_rcvd,
						coordinator_rcvd,
						let_sent,
						sec_order						
						)
					values
						(@candidate,
						@exam_type,
						@let_type,				
						ltrim(rtrim(@official)),
						ltrim(rtrim(@coordinator)),
						case ltrim(rtrim(@let_rcvd)) when '' then null else @let_rcvd end,
						case ltrim(rtrim(@coordinator_rcvd)) when '' then null else @coordinator_rcvd end,
						@let_sent,
						@sec_order)	
				end	
			end							
		end
		
		if ISNULL(@id_code,'')!=''
		begin
			update AppRefLet set 
				id_code=@id_code
				where candidate=@candidate and exam_type=@exam_type and sec_order=@sec_order and isnull(id_code,'')!=@id_code;
		end

		select id_code,dbo.udf_get_email(candidate) email,isnull(coordinator_email,'') coordinator_email from appreflet where id=@id;
	end