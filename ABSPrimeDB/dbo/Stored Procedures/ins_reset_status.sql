CREATE proc [dbo].[ins_reset_status]
	@id		numeric,
	@app_id	tinyint,
        @status tinyint,
	@modifier	varchar(30)=null
as 
	DECLARE @candidate varchar(6),@exam_type varchar(8),@year int,@log as varchar(60),@datereceive smalldatetime
	SET NOCOUNT ON
	set @datereceive=case @status when 0 then null else getdate() end
	
	SELECT @candidate=candidate,@exam_type=cast(year as varchar)+exam+type,@year=year from track where id=@id
	
	UPDATE app_response set status=@status where candidate=@candidate and exam_type=@exam_type and app_id=@app_id
	if @app_id=0
	begin
		UPDATE track set app_receive=@datereceive,note=case isnull(note,'') when '' then '' else note+char(13) end + 'Form #0 status set to '+cast(@status as varchar)+' '+convert(varchar,getdate(),101),modified=getdate(),modifier='xul' where id=@id;
		if (CHARINDEX('MC',@exam_type)>0)
		begin
			if (@status=0)
			begin
				update cme set usedwith='false' where candidate=@candidate and isnull(usedwith,'') = 'true';
				update cme set usedwith='true' where candidate=@candidate and isnull(usedwith,'') = @exam_type;
			end
			else
			begin
				update cme set usedwith=@exam_type where candidate=@candidate and isnull(usedwith,'') in ('','true');
				update cme set usedwith='true' where candidate=@candidate and isnull(usedwith,'') = 'false';
			end
		end
	end
	else
	begin
		UPDATE track set oper_receive=@datereceive,note=note+case isnull(note,'') when '' then '' else char(13) end + 'Form #1 status set to '+cast(@status as varchar)+' '+convert(varchar,getdate(),101),modified=getdate(),modifier='xul' where id=@id	
	end         

	set @log=@candidate+':Final removed for '+case @app_id when 0 then 'Gen.' when 1 then 'OE.' end+'App.,'+cast(@year as varchar(4))+@exam_type
        exec ins_log @log,null,@modifier
    
        select case year(isnull(@datereceive,'')) when 1900 then '' else isnull(convert(varchar(10),@datereceive,101),'') end datereceive,
        	@candidate candidate,@exam_type exam_type, @app_id app_id