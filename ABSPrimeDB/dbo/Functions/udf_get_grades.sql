CREATE function udf_get_grades(@examcode varchar(7))

RETURNS TABLE
AS

RETURN (

SELECT 
	timecode,dbs,
	candidate, 
	CASE WHEN B1>0 THEN B1 ELSE B1_CASE1_SCORE+B1_CASE2_SCORE+B1_CASE3_SCORE+B1_CASE4_SCORE END +
	CASE WHEN A1>0 THEN A1 ELSE A1_CASE1_SCORE+A1_CASE2_SCORE+A1_CASE3_SCORE+A1_CASE4_SCORE END +
	CASE WHEN B2>0 THEN B2 ELSE B2_CASE1_SCORE+B2_CASE2_SCORE+B2_CASE3_SCORE+B2_CASE4_SCORE END +
	CASE WHEN A2>0 THEN A2 ELSE A2_CASE1_SCORE+A2_CASE2_SCORE+A2_CASE3_SCORE+A2_CASE4_SCORE END +
	CASE WHEN B3>0 THEN B3 ELSE B3_CASE1_SCORE+B3_CASE2_SCORE+B3_CASE3_SCORE+B3_CASE4_SCORE END +
	CASE WHEN A3>0 THEN A3 ELSE A3_CASE1_SCORE+A3_CASE2_SCORE+A3_CASE3_SCORE+A3_CASE4_SCORE END +
	CASE WHEN B4>0 THEN B4 ELSE B4_CASE1_SCORE+B4_CASE2_SCORE+B4_CASE3_SCORE+B4_CASE4_SCORE END +
	CASE WHEN A4>0 THEN A4 ELSE A4_CASE1_SCORE+A4_CASE2_SCORE+A4_CASE3_SCORE+A4_CASE4_SCORE END +
	CASE WHEN B5>0 THEN B5 ELSE B5_CASE1_SCORE+B5_CASE2_SCORE+B5_CASE3_SCORE+B5_CASE4_SCORE END +
	CASE WHEN A5>0 THEN A5 ELSE A5_CASE1_SCORE+A5_CASE2_SCORE+A5_CASE3_SCORE+A5_CASE4_SCORE END 
	rawscore,
	
	ISNULL(B1_ADJ,0)+ISNULL(A1_ADJ,0)+ISNULL(B2_ADJ,0)+ISNULL(A2_ADJ,0)+ISNULL(B3_ADJ,0)+ISNULL(A3_ADJ,0)+ISNULL(B4_ADJ,0)+ISNULL(A4_ADJ,0)+ISNULL(B5_ADJ,0)+ISNULL(A5_ADJ,0) adjustedscore,
	
	case when (B1_ADJ+A1_ADJ+B2_ADJ+A2_ADJ+B3_ADJ+A3_ADJ+B4_ADJ+A4_ADJ+B5_ADJ+A5_ADJ)>=
	        (case exam when 'P' then 93 when 'O' then 74 else 56 end)
	     then 'P' 
	     when (B1_ADJ+A1_ADJ+B2_ADJ+A2_ADJ+B3_ADJ+A3_ADJ+B4_ADJ+A4_ADJ+B5_ADJ+A5_ADJ)=0 
	     then '' 
	     else 'F' 
	end result_adj,
	
	case when (
			CASE WHEN B1>0 THEN B1 ELSE B1_CASE1_SCORE+B1_CASE2_SCORE+B1_CASE3_SCORE+B1_CASE4_SCORE END +
			CASE WHEN A1>0 THEN A1 ELSE A1_CASE1_SCORE+A1_CASE2_SCORE+A1_CASE3_SCORE+A1_CASE4_SCORE END +
			CASE WHEN B2>0 THEN B2 ELSE B2_CASE1_SCORE+B2_CASE2_SCORE+B2_CASE3_SCORE+B2_CASE4_SCORE END +
			CASE WHEN A2>0 THEN A2 ELSE A2_CASE1_SCORE+A2_CASE2_SCORE+A2_CASE3_SCORE+A2_CASE4_SCORE END +
			CASE WHEN B3>0 THEN B3 ELSE B3_CASE1_SCORE+B3_CASE2_SCORE+B3_CASE3_SCORE+B3_CASE4_SCORE END +
			CASE WHEN A3>0 THEN A3 ELSE A3_CASE1_SCORE+A3_CASE2_SCORE+A3_CASE3_SCORE+A3_CASE4_SCORE END +
			CASE WHEN B4>0 THEN B4 ELSE B4_CASE1_SCORE+B4_CASE2_SCORE+B4_CASE3_SCORE+B4_CASE4_SCORE END +
			CASE WHEN A4>0 THEN A4 ELSE A4_CASE1_SCORE+A4_CASE2_SCORE+A4_CASE3_SCORE+A4_CASE4_SCORE END +
			CASE WHEN B5>0 THEN B5 ELSE B5_CASE1_SCORE+B5_CASE2_SCORE+B5_CASE3_SCORE+B5_CASE4_SCORE END +
			CASE WHEN A5>0 THEN A5 ELSE A5_CASE1_SCORE+A5_CASE2_SCORE+A5_CASE3_SCORE+A5_CASE4_SCORE END 
			)>=
	        (case exam when 'P' then 93 when 'O' then 74 else 56 end)
	     then 'P' 
	     when (
			CASE WHEN B1>0 THEN B1 ELSE B1_CASE1_SCORE+B1_CASE2_SCORE+B1_CASE3_SCORE+B1_CASE4_SCORE END +
			CASE WHEN A1>0 THEN A1 ELSE A1_CASE1_SCORE+A1_CASE2_SCORE+A1_CASE3_SCORE+A1_CASE4_SCORE END +
			CASE WHEN B2>0 THEN B2 ELSE B2_CASE1_SCORE+B2_CASE2_SCORE+B2_CASE3_SCORE+B2_CASE4_SCORE END +
			CASE WHEN A2>0 THEN A2 ELSE A2_CASE1_SCORE+A2_CASE2_SCORE+A2_CASE3_SCORE+A2_CASE4_SCORE END +
			CASE WHEN B3>0 THEN B3 ELSE B3_CASE1_SCORE+B3_CASE2_SCORE+B3_CASE3_SCORE+B3_CASE4_SCORE END +
			CASE WHEN A3>0 THEN A3 ELSE A3_CASE1_SCORE+A3_CASE2_SCORE+A3_CASE3_SCORE+A3_CASE4_SCORE END +
			CASE WHEN B4>0 THEN B4 ELSE B4_CASE1_SCORE+B4_CASE2_SCORE+B4_CASE3_SCORE+B4_CASE4_SCORE END +
			CASE WHEN A4>0 THEN A4 ELSE A4_CASE1_SCORE+A4_CASE2_SCORE+A4_CASE3_SCORE+A4_CASE4_SCORE END +
			CASE WHEN B5>0 THEN B5 ELSE B5_CASE1_SCORE+B5_CASE2_SCORE+B5_CASE3_SCORE+B5_CASE4_SCORE END +
			CASE WHEN A5>0 THEN A5 ELSE A5_CASE1_SCORE+A5_CASE2_SCORE+A5_CASE3_SCORE+A5_CASE4_SCORE END 
			)=0 
	     then '' 
	     else 'F' 
	end result_raw,
	
	CASE WHEN B1>0 THEN B1 ELSE B1_CASE1_SCORE+B1_CASE2_SCORE+B1_CASE3_SCORE+B1_CASE4_SCORE END B1,
	ISNULL(B1_ADJ,0) B1_ADJ,
	case B1_ID when 0 then '0'+cast(B1_ID_PLANNED as varchar(6)) else '0'+cast(B1_ID as varchar(6)) end as B1_ID,
	B1_INTID,
	B1_CASE1,B1_CASE1_SCORE,B1_CASE2,B1_CASE2_SCORE,B1_CASE3,B1_CASE3_SCORE,B1_CASE4,B1_CASE4_SCORE,
	B1_CMP_NUM_CASE_SCORES,B1_CMP_NUM_CASES,
	
	CASE WHEN A1>0 THEN A1 ELSE A1_CASE1_SCORE+A1_CASE2_SCORE+A1_CASE3_SCORE+A1_CASE4_SCORE END A1,
	ISNULL(A1_ADJ,0) A1_ADJ,
	case A1_ID when 0 then '0'+cast(A1_ID_PLANNED as varchar(6)) else '0'+cast(A1_ID as varchar(6)) end as A1_ID,
	A1_INTID,
	A1_CASE1,A1_CASE1_SCORE,A1_CASE2,A1_CASE2_SCORE,A1_CASE3,A1_CASE3_SCORE,A1_CASE4,A1_CASE4_SCORE,
	A1_CMP_NUM_CASE_SCORES,A1_CMP_NUM_CASES,
	
	CASE WHEN B2>0 THEN B2 ELSE B2_CASE1_SCORE+B2_CASE2_SCORE+B2_CASE3_SCORE+B2_CASE4_SCORE END B2,
	ISNULL(B2_ADJ,0) B2_ADJ,
	case B2_ID when 0 then '0'+cast(B2_ID_PLANNED as varchar(6)) else '0'+cast(B2_ID as varchar(6)) end as B2_ID,
	B2_INTID,
	B2_CASE1,B2_CASE1_SCORE,B2_CASE2,B2_CASE2_SCORE,B2_CASE3,B2_CASE3_SCORE,B2_CASE4,B2_CASE4_SCORE,
	B2_CMP_NUM_CASE_SCORES,B2_CMP_NUM_CASES,
	
	CASE WHEN A2>0 THEN A2 ELSE A2_CASE1_SCORE+A2_CASE2_SCORE+A2_CASE3_SCORE+A2_CASE4_SCORE END A2,
	ISNULL(A2_ADJ,0) A2_ADJ,
	case A2_ID when 0 then '0'+cast(A2_ID_PLANNED as varchar(6)) else '0'+cast(A2_ID as varchar(6)) end as A2_ID,
	A2_INTID,
	A2_CASE1,A2_CASE1_SCORE,A2_CASE2,A2_CASE2_SCORE,A2_CASE3,A2_CASE3_SCORE,A2_CASE4,A2_CASE4_SCORE,
	A2_CMP_NUM_CASE_SCORES,A2_CMP_NUM_CASES,
	
	CASE WHEN B3>0 THEN B3 ELSE B3_CASE1_SCORE+B3_CASE2_SCORE+B3_CASE3_SCORE+B3_CASE4_SCORE END B3,
	ISNULL(B3_ADJ,0) B3_ADJ,
	case B3_ID when 0 then '0'+cast(B3_ID_PLANNED as varchar(6)) else '0'+cast(B3_ID as varchar(6)) end as B3_ID,
	B3_INTID,
	B3_CASE1,B3_CASE1_SCORE,B3_CASE2,B3_CASE2_SCORE,B3_CASE3,B3_CASE3_SCORE,B3_CASE4,B3_CASE4_SCORE,
	B3_CMP_NUM_CASE_SCORES,B3_CMP_NUM_CASES,
	
	CASE WHEN A3>0 THEN A3 ELSE A3_CASE1_SCORE+A3_CASE2_SCORE+A3_CASE3_SCORE+A3_CASE4_SCORE END A3,
	ISNULL(A3_ADJ,0) A3_ADJ,
	case A3_ID when 0 then '0'+cast(A3_ID_PLANNED as varchar(6)) else '0'+cast(A3_ID as varchar(6)) end as A3_ID,
	A3_INTID,
	A3_CASE1,A3_CASE1_SCORE,A3_CASE2,A3_CASE2_SCORE,A3_CASE3,A3_CASE3_SCORE,A3_CASE4,A3_CASE4_SCORE,
	A3_CMP_NUM_CASE_SCORES,A3_CMP_NUM_CASES,
	
	CASE WHEN B4>0 THEN B4 ELSE B4_CASE1_SCORE+B4_CASE2_SCORE+B4_CASE3_SCORE+B4_CASE4_SCORE END B4,
	ISNULL(B4_ADJ,0) B4_ADJ,
	case B4_ID when 0 then '0'+cast(B4_ID_PLANNED as varchar(6)) else '0'+cast(B4_ID as varchar(6)) end as B4_ID,
	B4_INTID,
	B4_CASE1,B4_CASE1_SCORE,B4_CASE2,B4_CASE2_SCORE,B4_CASE3,B4_CASE3_SCORE,B4_CASE4,B4_CASE4_SCORE,
	B4_CMP_NUM_CASE_SCORES,B4_CMP_NUM_CASES,
	
	CASE WHEN A4>0 THEN A4 ELSE A4_CASE1_SCORE+A4_CASE2_SCORE+A4_CASE3_SCORE+A4_CASE4_SCORE END A4,
	ISNULL(A4_ADJ,0) A4_ADJ,
	case A4_ID when 0 then '0'+cast(A4_ID_PLANNED as varchar(6)) else '0'+cast(A4_ID as varchar(6)) end as A4_ID,
	A4_INTID,
	A4_CASE1,A4_CASE1_SCORE,A4_CASE2,A4_CASE2_SCORE,A4_CASE3,A4_CASE3_SCORE,A4_CASE4,A4_CASE4_SCORE,
	A4_CMP_NUM_CASE_SCORES,A4_CMP_NUM_CASES,
	
	CASE WHEN B5>0 THEN B5 ELSE B5_CASE1_SCORE+B5_CASE2_SCORE+B5_CASE3_SCORE+B5_CASE4_SCORE END B5,
	ISNULL(B5_ADJ,0) B5_ADJ,
	case B5_ID when 0 then '0'+cast(B5_ID_PLANNED as varchar(6)) else '0'+cast(B5_ID as varchar(6)) end as B5_ID,
	B5_INTID,
	B5_CASE1,B5_CASE1_SCORE,B5_CASE2,B5_CASE2_SCORE,B5_CASE3,B5_CASE3_SCORE,B5_CASE4,B5_CASE4_SCORE,
	B5_CMP_NUM_CASE_SCORES,B5_CMP_NUM_CASES,
	
	CASE WHEN A5>0 THEN A5 ELSE A5_CASE1_SCORE+A5_CASE2_SCORE+A5_CASE3_SCORE+A5_CASE4_SCORE END A5,
	ISNULL(A5_ADJ,0) A5_ADJ,
	case A5_ID when 0 then '0'+cast(A5_ID_PLANNED as varchar(6)) else '0'+cast(A5_ID as varchar(6)) end as A5_ID,
	A5_INTID,
	A5_CASE1,A5_CASE1_SCORE,A5_CASE2,A5_CASE2_SCORE,A5_CASE3,A5_CASE3_SCORE,A5_CASE4,A5_CASE4_SCORE,
	A5_CMP_NUM_CASE_SCORES,A5_CMP_NUM_CASES,
	
	isnull(rtrim(result),'') result,isnull(result_comment,'') as result_comment,exam_id,day,briefing,session,
	examcode,exam
from 
(
SELECT a.timecode,a.day+a.briefing+a.session as dbs,a.candidate,
	MAX(CASE role WHEN 'B1' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS B1,
	MAX(CASE role WHEN 'B1' THEN cast(examiner as numeric) ELSE 0 END) AS B1_ID,
	MAX(CASE role WHEN 'B1' THEN exam_score.id ELSE 0 END) AS B1_INTID,
	MAX(CASE role WHEN 'B1' THEN score_adjusted ELSE 0 END) AS B1_ADJ,
	MAX(CASE role WHEN 'B1' THEN exam_score.case_1 ELSE '' END) AS B1_CASE1,
	MAX(CASE role WHEN 'B1' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS B1_CASE1_SCORE,
	MAX(CASE role WHEN 'B1' THEN exam_score.case_2 ELSE '' END) AS B1_CASE2,
	MAX(CASE role WHEN 'B1' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS B1_CASE2_SCORE,
	MAX(CASE role WHEN 'B1' THEN exam_score.case_3 ELSE '' END) AS B1_CASE3,
	MAX(CASE role WHEN 'B1' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS B1_CASE3_SCORE,
	MAX(CASE role WHEN 'B1' THEN exam_score.case_4 ELSE '' END) AS B1_CASE4,
	MAX(CASE role WHEN 'B1' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS B1_CASE4_SCORE,
	MAX(CASE role WHEN 'B1' THEN cmp_num_case_scores ELSE 0 END) AS B1_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'B1' THEN cmp_num_cases ELSE 0 END) AS B1_CMP_NUM_CASES,

   	MAX(CASE role WHEN 'A1' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS A1,
   	MAX(CASE role WHEN 'A1' THEN cast(examiner as numeric) ELSE 0 END) AS A1_ID,
	MAX(CASE role WHEN 'A1' THEN exam_score.id ELSE 0 END) AS A1_INTID,
	MAX(CASE role WHEN 'A1' THEN score_adjusted ELSE 0 END) AS A1_ADJ,
	MAX(CASE role WHEN 'A1' THEN exam_score.case_1 ELSE '' END) AS A1_CASE1,
	MAX(CASE role WHEN 'A1' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS A1_CASE1_SCORE,
	MAX(CASE role WHEN 'A1' THEN exam_score.case_2 ELSE '' END) AS A1_CASE2,
	MAX(CASE role WHEN 'A1' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS A1_CASE2_SCORE,
	MAX(CASE role WHEN 'A1' THEN exam_score.case_3 ELSE '' END) AS A1_CASE3,
	MAX(CASE role WHEN 'A1' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS A1_CASE3_SCORE,
	MAX(CASE role WHEN 'A1' THEN exam_score.case_4 ELSE '' END) AS A1_CASE4,
	MAX(CASE role WHEN 'A1' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS A1_CASE4_SCORE,
	MAX(CASE role WHEN 'A1' THEN cmp_num_case_scores ELSE 0 END) AS A1_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'A1' THEN cmp_num_cases ELSE 0 END) AS A1_CMP_NUM_CASES,	

	MAX(CASE role WHEN 'B2' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS B2,
	MAX(CASE role WHEN 'B2' THEN cast(examiner as numeric) ELSE 0 END) AS B2_ID,
	MAX(CASE role WHEN 'B2' THEN exam_score.id ELSE 0 END) AS B2_INTID,
	MAX(CASE role WHEN 'B2' THEN score_adjusted ELSE 0 END) AS B2_ADJ,
	MAX(CASE role WHEN 'B2' THEN exam_score.case_1 ELSE '' END) AS B2_CASE1,
	MAX(CASE role WHEN 'B2' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS B2_CASE1_SCORE,
	MAX(CASE role WHEN 'B2' THEN exam_score.case_2 ELSE '' END) AS B2_CASE2,
	MAX(CASE role WHEN 'B2' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS B2_CASE2_SCORE,
	MAX(CASE role WHEN 'B2' THEN exam_score.case_3 ELSE '' END) AS B2_CASE3,
	MAX(CASE role WHEN 'B2' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS B2_CASE3_SCORE,
	MAX(CASE role WHEN 'B2' THEN exam_score.case_4 ELSE '' END) AS B2_CASE4,
	MAX(CASE role WHEN 'B2' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS B2_CASE4_SCORE,
	MAX(CASE role WHEN 'B2' THEN cmp_num_case_scores ELSE 0 END) AS B2_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'B2' THEN cmp_num_cases ELSE 0 END) AS B2_CMP_NUM_CASES,	

	MAX(CASE role WHEN 'A2' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS A2,
	MAX(CASE role WHEN 'A2' THEN cast(examiner as numeric) ELSE 0 END) AS A2_ID,
	MAX(CASE role WHEN 'A2' THEN exam_score.id ELSE 0 END) AS A2_INTID,
	MAX(CASE role WHEN 'A2' THEN score_adjusted ELSE 0 END) AS A2_ADJ,
	MAX(CASE role WHEN 'A2' THEN exam_score.case_1 ELSE '' END) AS A2_CASE1,
	MAX(CASE role WHEN 'A2' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS A2_CASE1_SCORE,
	MAX(CASE role WHEN 'A2' THEN exam_score.case_2 ELSE '' END) AS A2_CASE2,
	MAX(CASE role WHEN 'A2' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS A2_CASE2_SCORE,
	MAX(CASE role WHEN 'A2' THEN exam_score.case_3 ELSE '' END) AS A2_CASE3,
	MAX(CASE role WHEN 'A2' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS A2_CASE3_SCORE,
	MAX(CASE role WHEN 'A2' THEN exam_score.case_4 ELSE '' END) AS A2_CASE4,
	MAX(CASE role WHEN 'A2' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS A2_CASE4_SCORE,
	MAX(CASE role WHEN 'A2' THEN cmp_num_case_scores ELSE 0 END) AS A2_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'A2' THEN cmp_num_cases ELSE 0 END) AS A2_CMP_NUM_CASES,	

	MAX(CASE role WHEN 'B3' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS B3,
	MAX(CASE role WHEN 'B3' THEN cast(examiner as numeric) ELSE 0 END) AS B3_ID,
	MAX(CASE role WHEN 'B3' THEN exam_score.id ELSE 0 END) AS B3_INTID,
	MAX(CASE role WHEN 'B3' THEN score_adjusted ELSE 0 END) AS B3_ADJ,
	MAX(CASE role WHEN 'B3' THEN exam_score.case_1 ELSE '' END) AS B3_CASE1,
	MAX(CASE role WHEN 'B3' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS B3_CASE1_SCORE,
	MAX(CASE role WHEN 'B3' THEN exam_score.case_2 ELSE '' END) AS B3_CASE2,
	MAX(CASE role WHEN 'B3' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS B3_CASE2_SCORE,
	MAX(CASE role WHEN 'B3' THEN exam_score.case_3 ELSE '' END) AS B3_CASE3,
	MAX(CASE role WHEN 'B3' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS B3_CASE3_SCORE,
	MAX(CASE role WHEN 'B3' THEN exam_score.case_4 ELSE '' END) AS B3_CASE4,
	MAX(CASE role WHEN 'B3' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS B3_CASE4_SCORE,
	MAX(CASE role WHEN 'B3' THEN cmp_num_case_scores ELSE 0 END) AS B3_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'B3' THEN cmp_num_cases ELSE 0 END) AS B3_CMP_NUM_CASES,	

	MAX(CASE role WHEN 'A3' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS A3,
	MAX(CASE role WHEN 'A3' THEN cast(examiner as numeric) ELSE 0 END) AS A3_ID,
	MAX(CASE role WHEN 'A3' THEN exam_score.id ELSE 0 END) AS A3_INTID,
	MAX(CASE role WHEN 'A3' THEN score_adjusted ELSE 0 END) AS A3_ADJ,
	MAX(CASE role WHEN 'A3' THEN exam_score.case_1 ELSE '' END) AS A3_CASE1,
	MAX(CASE role WHEN 'A3' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS A3_CASE1_SCORE,
	MAX(CASE role WHEN 'A3' THEN exam_score.case_2 ELSE '' END) AS A3_CASE2,
	MAX(CASE role WHEN 'A3' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS A3_CASE2_SCORE,
	MAX(CASE role WHEN 'A3' THEN exam_score.case_3 ELSE '' END) AS A3_CASE3,
	MAX(CASE role WHEN 'A3' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS A3_CASE3_SCORE,
	MAX(CASE role WHEN 'A3' THEN exam_score.case_4 ELSE '' END) AS A3_CASE4,
	MAX(CASE role WHEN 'A3' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS A3_CASE4_SCORE,	
	MAX(CASE role WHEN 'A3' THEN cmp_num_case_scores ELSE 0 END) AS A3_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'A3' THEN cmp_num_cases ELSE 0 END) AS A3_CMP_NUM_CASES,

	MAX(CASE role WHEN 'B4' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS B4,
	MAX(CASE role WHEN 'B4' THEN cast(examiner as numeric) ELSE 0 END) AS B4_ID,
	MAX(CASE role WHEN 'B4' THEN exam_score.id ELSE 0 END) AS B4_INTID,
	MAX(CASE role WHEN 'B4' THEN score_adjusted ELSE 0 END) AS B4_ADJ,
	MAX(CASE role WHEN 'B4' THEN exam_score.case_1 ELSE '' END) AS B4_CASE1,
	MAX(CASE role WHEN 'B4' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS B4_CASE1_SCORE,
	MAX(CASE role WHEN 'B4' THEN exam_score.case_2 ELSE '' END) AS B4_CASE2,
	MAX(CASE role WHEN 'B4' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS B4_CASE2_SCORE,
	MAX(CASE role WHEN 'B4' THEN exam_score.case_3 ELSE '' END) AS B4_CASE3,
	MAX(CASE role WHEN 'B4' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS B4_CASE3_SCORE,
	MAX(CASE role WHEN 'B4' THEN exam_score.case_4 ELSE '' END) AS B4_CASE4,
	MAX(CASE role WHEN 'B4' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS B4_CASE4_SCORE,
	MAX(CASE role WHEN 'B4' THEN cmp_num_case_scores ELSE 0 END) AS B4_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'B4' THEN cmp_num_cases ELSE 0 END) AS B4_CMP_NUM_CASES,		

	MAX(CASE role WHEN 'A4' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS A4,
	MAX(CASE role WHEN 'A4' THEN cast(examiner as numeric) ELSE 0 END) AS A4_ID,
	MAX(CASE role WHEN 'A4' THEN exam_score.id ELSE 0 END) AS A4_INTID,
	MAX(CASE role WHEN 'A4' THEN score_adjusted ELSE 0 END) AS A4_ADJ,
	MAX(CASE role WHEN 'A4' THEN exam_score.case_1 ELSE '' END) AS A4_CASE1,
	MAX(CASE role WHEN 'A4' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS A4_CASE1_SCORE,
	MAX(CASE role WHEN 'A4' THEN exam_score.case_2 ELSE '' END) AS A4_CASE2,
	MAX(CASE role WHEN 'A4' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS A4_CASE2_SCORE,
	MAX(CASE role WHEN 'A4' THEN exam_score.case_3 ELSE '' END) AS A4_CASE3,
	MAX(CASE role WHEN 'A4' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS A4_CASE3_SCORE,
	MAX(CASE role WHEN 'A4' THEN exam_score.case_4 ELSE '' END) AS A4_CASE4,
	MAX(CASE role WHEN 'A4' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS A4_CASE4_SCORE,
	MAX(CASE role WHEN 'A4' THEN cmp_num_case_scores ELSE 0 END) AS A4_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'A4' THEN cmp_num_cases ELSE 0 END) AS A4_CMP_NUM_CASES,	

	MAX(CASE role WHEN 'B5' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS B5,
	MAX(CASE role WHEN 'B5' THEN cast(examiner as numeric) ELSE 0 END) AS B5_ID,
	MAX(CASE role WHEN 'B5' THEN exam_score.id ELSE 0 END) AS B5_INTID,
	MAX(CASE role WHEN 'B5' THEN score_adjusted ELSE 0 END) AS B5_ADJ,
	MAX(CASE role WHEN 'B5' THEN exam_score.case_1 ELSE '' END) AS B5_CASE1,
	MAX(CASE role WHEN 'B5' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS B5_CASE1_SCORE,
	MAX(CASE role WHEN 'B5' THEN exam_score.case_2 ELSE '' END) AS B5_CASE2,
	MAX(CASE role WHEN 'B5' THEN ISNULL(exam_score.case_2_score,0) ELSE 0 END) AS B5_CASE2_SCORE,
	MAX(CASE role WHEN 'B5' THEN exam_score.case_3 ELSE '' END) AS B5_CASE3,
	MAX(CASE role WHEN 'B5' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS B5_CASE3_SCORE,
	MAX(CASE role WHEN 'B5' THEN exam_score.case_4 ELSE '' END) AS B5_CASE4,
	MAX(CASE role WHEN 'B5' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS B5_CASE4_SCORE,
	MAX(CASE role WHEN 'B5' THEN cmp_num_case_scores ELSE 0 END) AS B5_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'B5' THEN cmp_num_cases ELSE 0 END) AS B5_CMP_NUM_CASES,	

	MAX(CASE role WHEN 'A5' THEN ISNULL(exam_score.score,0) ELSE 0 END) AS A5,
	MAX(CASE role WHEN 'A5' THEN cast(examiner as numeric) ELSE 0 END) AS A5_ID,
	MAX(CASE role WHEN 'A5' THEN exam_score.id ELSE 0 END) AS A5_INTID,
	MAX(CASE role WHEN 'A5' THEN score_adjusted ELSE 0 END) AS A5_ADJ,
	MAX(CASE role WHEN 'A5' THEN exam_score.case_1 ELSE '' END) AS A5_CASE1,
	MAX(CASE role WHEN 'A5' THEN ISNULL(exam_score.case_1_score,0) ELSE 0 END) AS A5_CASE1_SCORE,
	MAX(CASE role WHEN 'A5' THEN exam_score.case_2 ELSE '' END) AS A5_CASE2,
	MAX(CASE role WHEN 'A5' THEN ISNULL(ISNULL(exam_score.case_2_score,0),0) ELSE 0 END) AS A5_CASE2_SCORE,
	MAX(CASE role WHEN 'A5' THEN exam_score.case_3 ELSE '' END) AS A5_CASE3,
	MAX(CASE role WHEN 'A5' THEN ISNULL(exam_score.case_3_score,0) ELSE 0 END) AS A5_CASE3_SCORE,
	MAX(CASE role WHEN 'A5' THEN exam_score.case_4 ELSE '' END) AS A5_CASE4,
	MAX(CASE role WHEN 'A5' THEN ISNULL(exam_score.case_4_score,0) ELSE 0 END) AS A5_CASE4_SCORE,
	MAX(CASE role WHEN 'A5' THEN cmp_num_case_scores ELSE 0 END) AS A5_CMP_NUM_CASE_SCORES,
	MAX(CASE role WHEN 'A5' THEN cmp_num_cases ELSE 0 END) AS A5_CMP_NUM_CASES,	

	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 0 THEN cast(board_examiner as numeric) ELSE 0 END) AS B1_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 0 THEN cast(assoc_examiner as numeric) ELSE 0 END) AS A1_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 1 THEN cast(board_examiner as numeric) ELSE 0 END) AS B2_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 1 THEN cast(assoc_examiner as numeric) ELSE 0 END) AS A2_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 2 THEN cast(board_examiner as numeric) ELSE 0 END) AS B3_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 2 THEN cast(assoc_examiner as numeric) ELSE 0 END) AS A3_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 3 THEN cast(board_examiner as numeric) ELSE 0 END) AS B4_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 3 THEN cast(assoc_examiner as numeric) ELSE 0 END) AS A4_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 4 THEN cast(board_examiner as numeric) ELSE 0 END) AS B5_ID_PLANNED,
	MAX(CASE (c.row-1) % (case a.exam when 'P' then 5 when 'O' then 4 else 3 end) WHEN 4 THEN cast(assoc_examiner as numeric) ELSE 0 END) AS A5_ID_PLANNED,
    
	result ,result_comment,a.id as exam_id,a.day,a.briefing,a.session,a.examcode,a.exam
    
FROM exam a 
	LEFT JOIN exam_score on a.id=exam_score.exam_id 
	LEFT JOIN exam_timeslot c on a.timecode=c.timecode and c.exam=a.exam 
	LEFT JOIN exam_teams_planned d on c.col=d.col and a.examcode=d.examcode and d.day=a.day
	INNER JOIN V_ABMS_SURGEON b on b.board_unique_id=a.candidate 
where a.year>2003 and a.examcode=@examcode and a.timecode is not null and a.day+a.briefing+a.session is not null
GROUP BY a.exam,a.examcode,a.timecode,a.day+a.briefing+a.session,a.candidate,result,vote,result_comment,a.id,a.day,a.briefing,a.session 
) as info)