CREATE proc ins_grades_XML
	@doc text
as 
	DECLARE @idoc int
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	SET NOCOUNT ON

		DELETE exam_score 
	FROM exam_score,(SELECT id,role
		FROM OPENXML (@idoc, '/xml/data/row[count(@role)>0]',1) 
		with (
			id	numeric,
			role	char(2))) info
	WHERE exam_score.exam_id=info.id and exam_score.role=info.role

		INSERT INTO exam_score (exam_id,examiner,score,role) 
	SELECT 	id,
		examiner,
		case rtrim(score) when '' then null else cast(score as numeric(2,1)) end score,
		role
	FROM OPENXML (@idoc, '/xml/data/row[count(@role)>0]',1) 
	with (
		id			numeric,
		examiner		char(6),
		score			numeric(2,1),
		role			char(2))
	
		UPDATE Exam set 
		Result=info.result,
                                result_comment=info.result_comment 	FROM (SELECT 	id,
						result,
			result_comment 
		FROM OPENXML (@idoc, '/xml/data/row[count(@role)>0]',1) 
		with (
			id			numeric,
						result			char(1),	
			result_comment		varchar(1000))) info
	WHERE exam.id=info.id

	EXEC sp_xml_removedocument @idoc

	select @@error