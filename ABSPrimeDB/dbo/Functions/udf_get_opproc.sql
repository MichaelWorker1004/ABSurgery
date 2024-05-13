CREATE FUNCTION dbo.udf_get_opproc(
	@candidate as char(6),
	@exam_type as varchar(8)
)
RETURNS float
AS
BEGIN
	DECLARE @x xml,@result float
	SELECT @x = response FROM app_response WHERE candidate=@candidate and exam_type=@exam_type and app_id=1
	SELECT @result=ISNULL(@x.value('sum(//*[@oid])+sum(//q41[count(@oid)=0])+sum(//q42[count(@oid)=0])+sum(//q43[count(@oid)=0])+sum(//q44[count(@oid)=0])+sum(//q106[count(@oid)=0])+sum(//q107[count(@oid)=0])+sum(//q108[count(@oid)=0])+sum(//q109[count(@oid)=0])+sum(//q112[count(@oid)=0])+sum(//q113[count(@oid)=0])+sum(//q114[count(@oid)=0])+sum(//q182[count(@oid)=0])','float'),0)

	IF CHARINDEX('CR', @exam_type)>0 OR CHARINDEX('CC', @exam_type)>0
	BEGIN
		SET @result=@result+
			ISNULL(@x.value('count(//q134)','float'),0)+
			ISNULL(@x.value('count(//q143)','float'),0)+
			(SELECT count(*) FROM caselog WHERE candidate=@candidate AND (dbo.udf_get_academic_year('T')-year(created))<11)
	END

	RETURN @result
END