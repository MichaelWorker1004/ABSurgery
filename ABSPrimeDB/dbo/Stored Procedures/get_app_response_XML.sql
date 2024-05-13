--drop proc get_app_response_XML
CREATE proc [dbo].[get_app_response_XML]
	@candidate as char(6),
	@app_type as varchar(8),
	@app_id as tinyint,
	@version as tinyint
as
	SET NOCOUNT ON
	if (@app_id=15)--reply cards
	begin
		set @app_type='RCD'
	end
	select response,status from app_response where app_id=@app_id and candidate=@candidate and exam_type=@app_type and version=@version
	--select '<response candidate="'+@candidate+'" app_type="'+@app_type+'" app_id="'+ cast(@app_id as varchar(3))+'" />'