CREATE proc [dbo].[ins_exam_score]
	@exam_id 	numeric,
	@examiner 	char(6),
	@score 		numeric(9, 3),
	@role		char(2),
	@case_1		varchar(6),
	@case_1_score	numeric(9, 3),
	@case_2		varchar(6),
	@case_2_score	numeric(9, 3),
	@case_3		varchar(6),
	@case_3_score	numeric(9, 3),
	@case_4		varchar(6),
	@case_4_score	numeric(9, 3)
as 
	
	
	
	IF EXISTS(SELECT id FROM exam_score WHERE exam_id=@exam_id AND role=@role)
	BEGIN
		UPDATE exam_score SET 
			examiner=@examiner,score_adjusted=null,score=@score,
			case_1=ISNULL(@case_1,dbo.udf_get_case_num(@exam_id,@examiner, 1)), case_1_score=ISNULL(@case_1_score,0),
			case_2=ISNULL(@case_2,dbo.udf_get_case_num(@exam_id,@examiner, 2)), case_2_score=ISNULL(@case_2_score,0),
			case_3=ISNULL(@case_3,dbo.udf_get_case_num(@exam_id,@examiner, 3)), case_3_score=ISNULL(@case_3_score,0),
			case_4=ISNULL(@case_4,dbo.udf_get_case_num(@exam_id,@examiner, 4)), case_4_score=ISNULL(@case_4_score,0) 
		WHERE exam_id=@exam_id AND role=@role
	END
	ELSE
	BEGIN
		INSERT INTO exam_score	(exam_id, examiner, score, 					role,			case_1,															case_1_score,				case_2,															case_2_score,				case_3,															case_3_score,				case_4,															case_4_score) 
		VALUES        			(@exam_id,@examiner,@score,					@role,			ISNULL(@case_1,dbo.udf_get_case_num(@exam_id,@examiner, 1)),	ISNULL(@case_1_score,0),	ISNULL(@case_2,dbo.udf_get_case_num(@exam_id,@examiner, 2)),	ISNULL(@case_2_score,0),	ISNULL(@case_3,dbo.udf_get_case_num(@exam_id,@examiner, 3)),	ISNULL(@case_3_score,0),	ISNULL(@case_4,dbo.udf_get_case_num(@exam_id,@examiner, 4)),	ISNULL(@case_4_score,0))	
	END