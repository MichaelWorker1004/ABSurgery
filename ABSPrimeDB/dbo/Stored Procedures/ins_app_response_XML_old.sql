--drop proc ins_app_response_XML
CREATE proc [ins_app_response_XML_old]
	@candidate as char(6),
	@app_type as varchar(2),
	@app_id as tinyint,
	@version  as tinyint,
	@response as text,
	@status as tinyint=0

as
	SET NOCOUNT ON
	if exists(select app_id from app_response where app_id=@app_id and candidate=@candidate and exam_type=@app_type and version=@version)
	begin
		update app_response set response=@response,status=@status where app_id=@app_id and candidate=@candidate and exam_type=@app_type and version = @version
	end
	else
	begin
		insert into app_response (candidate,exam_type,app_id,version,response,status) values (@candidate,@app_type,@app_id,@version,@response,@status)
	end
	
	-- if (rtrim(@app_type)='P')
	-- begin
	-- 	exec  ins_pref_XML @candidate,@response
	-- end
	
	exec get_app_response_XML @candidate,@app_type,@app_id,@version