--drop proc ins_education_XML
CREATE proc ins_education_XML
	@cand varchar(6),@exam_type varchar(6),@doc text
as 
	DECLARE @idoc int

	DELETE FROM cmp_education WHERE exam_type=@exam_type AND candidate=@cand;	

	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc
		insert cmp_education (candidate,exam_type,date_from,date_to,rot_type,subj_area,prg_name,institution,other,rot_intl)  
		SELECT @cand,@exam_type,q19,q20,isnull(q34,'')+isnull(q316,''),q22,q18,q154,isnull(q32,''),isnull(q325,'') FROM OPENXML (@idoc, '//answer[q18][q19][q20]',10) with 
		(q19 smalldatetime,
		q20 smalldatetime,
		q34 varchar(50),
		q316 varchar(50),
		q22 varchar(500),
		q18 varchar(500),
		q154 varchar(500),
		q32 varchar(500),
		q325 varchar(3)
		) 
	
	EXEC sp_xml_removedocument @idoc