CREATE FUNCTION [dbo].[udf_get_app_eligibility_bin](@candidate varchar(6),@app_type as varchar(8))
RETURNS varchar(50)
AS
BEGIN
	declare @result varchar(50),@academic_year smallint
	set @result='';
	set @academic_year=dbo.udf_get_academic_year('T');	 				

if exists(select candidate from dscpl_action where candidate=@candidate and effective=1 and dscpl_code in ('CR','CS'))
	begin	
		set @result='4,'
	end	
if not exists (select candidate from surgeon where candidate=@candidate)
	begin
		set @result='1,'
	end

if right(@app_type,2)!='MC' and exists(select candidate from moc_eligibility where moc_eligibility.candidate=@candidate and num_cycles_non_compliant>0)
	begin	
		set @result='15,'
	end		

if exists(select candidate from surgeon_st where status_code='AO' and candidate=@candidate and CHARINDEX(@app_type, st_val)>0)
	begin	
		set @result=''
	end
else if exists(
		select app_send from track where 
		((app_send is not null and year(app_send)!=1900) or (app_approve is not null and year(app_approve)!=1900)) 
		and year=@academic_year and exam+type=@app_type
		and candidate=@candidate
	)
	begin	
		set @result=''
	end
else if (right(@app_type,2)='MC')
	begin 
		if (select min(expiration) from exam_pass where candidate=@candidate and date>'07/01/2005')=2099
		begin
			set  @result=@result+'13'
		end
		else
		begin
	        if not exists(select candidate from moc_eligibility where candidate=@candidate)
			begin
				set @result=@result+'12'
			end
		end
	end
	
else if (right(@app_type,2)='GB')
	begin 
		if not exists (select candidate from v_abms_cert where candidate=@candidate AND exam='G' AND CertificationExpireDate>getdate())
		begin
			set @result=@result+'9'
		end
	end	
	
else if (right(@app_type,2)='CN')
	begin 
		if 	not exists (select candidate from v_abms_cert where candidate=@candidate AND exam='C' AND CertificationExpireDate>getdate())
			OR
			exists (select candidate from track where candidate=@candidate and exam='C' and type='N' and year!=@academic_year and app_approve is not null)
		begin
			set @result=@result+'9'
		end
	end	

else if (@app_type='GQ')
	begin	
	select  @result=@result+
			case isnull(a.admissible,'') when '' then '' when 'N' then '9,' else '5,' end +
			case isnull(COALESCE (b.candidate,c.candidate),'') when '' then '' else '6,' end +
			case isnull(p.candidate,'') when '' then '8,' else '' end +
			case isnull(t.candidate,'') when '' then '' else '16,' end 
		from surgeon z 
		left join exam_eligibility a on a.candidate=z.candidate and a.exam+a.type='GW' 		
		left join track t on t.candidate=z.candidate and t.exam+t.type='SP' and t.app_approve is not null
		left join exam b on b.candidate=z.candidate and b.exam+b.type='GW' and b.result='P' 
		left join exam_pass c on c.candidate=z.candidate and c.exam+c.type='GW'
		left join candidate_program p on p.candidate+p.exam=z.candidate+'G' AND cast(p.compyear as INT) > (@academic_year-4)
		where z.candidate=@candidate
	end
	
else if (@app_type='CC' and @candidate like '8%')
	begin	
	select @result=@result+
			case isnull(a.admissible,'') when '' then '' when 'N' then '9,' else '5,' end +
			case isnull(COALESCE (b.candidate,c.candidate),'') when '' then '' else '6,' end +
			case isnull(p.candidate,'') when '' then '8,' else '' end 
		from surgeon z 
		left join exam_eligibility a on a.candidate=z.candidate and a.exam+a.type='CW' 		
		left join exam b on b.candidate=z.candidate and b.exam+b.type='CW' and b.result='P' 
		left join exam_pass c on c.candidate=z.candidate and c.exam+c.type='CW'
		left join candidate_program p on p.candidate+p.exam=z.candidate+'C'
		where z.candidate=@candidate
	end
/* JFF Disable Automatic Access to HPM Apps; Override Required Now
else if (@app_type='ZC')
	begin	
   	select @result=@result+
			case isnull(a.admissible,'') when '' then '' when 'N' then '9,' else '5,' end +                
			case isnull(b.candidate,'') when '' then '' else '6,' end +
			case isnull(c.candidate,'') when '' then '9,' else '' end 
		from surgeon z 
		left join exam_eligibility a on a.candidate=z.candidate and a.exam+a.type='ZW'
		left join exam b on b.candidate=z.candidate and b.exam='Z' and b.result='P' 
       		left join 
        			(select distinct candidate from exam_pass where exam='G' and isnull(expiration,0)>=@academic_year) c on c.candidate=z.candidate
		where z.candidate=@candidate
	end
*/
else if (@app_type='OQ' and @candidate like '8%')
	begin	
	select  @result=@result+
			case isnull(a.admissible,'') when '' then '' when 'N' then '9,' else '5,' end +
			case isnull(COALESCE (b.candidate,c.candidate),'') when '' then '' else '6,' end +
			case isnull(d.candidate,'') when '' then '20,' else '' end +
			case isnull(p.candidate,'') when '' then '8,' else '' end 
		from surgeon z 
		left join exam_eligibility a on a.candidate=z.candidate and a.exam+a.type='OW' 		
		left join exam b on b.candidate=z.candidate and b.exam+b.type='OW' and b.result='P' 
		left join exam_pass c on c.candidate=z.candidate and c.exam+c.type='OW'
		left join 
			(select candidate,max(expiration) expiration from exam_pass where exam='G' and type in ('O', 'R') group by candidate,exam) d
				on d.candidate=z.candidate and d.expiration >=@academic_year
		left join candidate_program p on p.candidate+p.exam=z.candidate+'O'	
		where z.candidate=@candidate
	end

else if (@app_type='PQ' and @candidate like '8%')
	begin	
	select  @result=@result+
			case isnull(a.admissible,'') when '' then '' when 'N' then '9,' else '5,' end +
			case isnull(COALESCE (b.candidate,c.candidate),'') when '' then '' else '6,' end +
			case isnull(d.candidate,'') when '' then '20,' else '' end +
			case isnull(p.candidate,'') when '' then '8,' else '' end 
		from surgeon z 
		left join exam_eligibility a on a.candidate=z.candidate and a.exam+a.type='PW' 		
		left join exam b on b.candidate=z.candidate and b.exam+b.type='PW' and b.result='P' 
		left join exam_pass c on c.candidate=z.candidate and c.exam+c.type='PW'
		left join 
			(select candidate,max(expiration) expiration from exam_pass where exam='G' and type in ('O', 'R') group by candidate,exam) d
				on d.candidate=z.candidate and d.expiration >=@academic_year
		left join candidate_program p on p.candidate+p.exam=z.candidate+'P'	
		where z.candidate=@candidate
	end

else if (@app_type='VQ' and @candidate like '8%')
	begin	
	select  @result=@result+
			case isnull(a.admissible,'') when '' then '' when 'N' then '9,' else '5,' end +
			case isnull(COALESCE (b.candidate,c.candidate),'') when '' then '' else '6,' end +
			case when COALESCE(d.candidate,e.candidate) is NULL then '18,' else '' end +
			case isnull(p.candidate,'') when '' then '8,' else '' end 
		from surgeon z 
		left join exam_eligibility a on a.candidate=z.candidate and a.exam+a.type='VW' 		
		left join exam b on b.candidate=z.candidate and b.exam+b.type='VW' and b.result='P' 
		left join exam_pass c on c.candidate=z.candidate and c.exam+c.type='VW'
		left join 
			(select candidate,max(expiration) expiration from exam_pass where exam='G' and type in ('O', 'R') group by candidate,exam) d
				on d.candidate=z.candidate 
		left join track e on e.candidate=z.candidate and e.exam+e.type in ('GQ', 'GR') and e.app_approve is not null
		left join candidate_program p on p.candidate+p.exam=z.candidate+'V' AND cast(p.compyear as INT) > (@academic_year-4)
		where z.candidate=@candidate
	end	

else if (@app_type in ('SP','VX','GX'))
	begin
		set @result=@result+'9'
	end	

else if (@app_type in ('GR','VR','PR','CR'))
	begin	
	select  @result=@result+
						case isnull(b.candidate,'') when '' then '7,' else '' end
		from surgeon z 
				left join 
			(select candidate,max(expiration) expiration from exam_pass where exam=left(@app_type,1) and expiration is not null group by candidate,exam) b
				on b.candidate=z.candidate
				left join surgeon_st on surgeon_st.candidate=z.candidate and status_code='SecureExam' and CHARINDEX(CAST(@academic_year as varchar)+@app_type, st_val)>0  
		where z.candidate=@candidate and surgeon_st.candidate IS NULL
	end	
	
else if (right(@app_type,1)='R' and @app_type!='ZR')
	begin	
	select  @result=@result+
						case isnull(b.candidate,'') when '' then '7,' else '' end
		from surgeon z 
				left join 
			(select candidate,max(expiration) expiration from exam_pass where exam=left(@app_type,1) and expiration is not null group by candidate,exam) b
				on b.candidate=z.candidate and b.expiration <(@academic_year+3)
				left join surgeon_st on surgeon_st.candidate=z.candidate and status_code='SecureExam' and CHARINDEX(CAST(@academic_year as varchar)+@app_type, st_val)>0 
		where z.candidate=@candidate and surgeon_st.candidate IS NULL
	end
else
	begin
		set @result=@result+'1,'
	end
	
if right(@result,1)=','
	begin
		set @result=left(@result,len(@result)-1)
	end
	else if len(@result)= 0 	begin
		set @result='true'
	end

	return @result
end