CREATE FUNCTION [dbo].[udf_get_case_num] (@exam_id numeric(9,0),@examiner char(6),@order_num tinyint)
RETURNS varchar(6)
AS
BEGIN
	BEGIN
		DECLARE @case_num varchar(6)

		SELECT
			@case_num=isnull(exam_cases_planned.case_num,'XXXXX')
		FROM
			(SELECT
				 exam_teams_planned.examcode,
				 exam_teams_planned.[day],
				 exam_teams_planned.board_examiner examiner,
				 'B'+case
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 3 end)=0 THEN '1'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 3 end)=1 THEN '2'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 3 end)=2 THEN '3'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 1 end)=3 THEN '4'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5                 else 1 end)=4 THEN '5'
				 end role,
				 exam_timeslot.timecode,
				 exam_timeslot.row
				FROM
					exam_teams_planned
				INNER JOIN exam_timeslot ON exam_teams_planned.col = exam_timeslot.col and exam_teams_planned.exam=exam_timeslot.exam
			UNION
				SELECT
				 exam_teams_planned.examcode,
				 exam_teams_planned.[day],
				 exam_teams_planned.assoc_examiner examiner,
				 'A'+case
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 3 end)=0 THEN '1'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 3 end)=1 THEN '2'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 3 end)=2 THEN '3'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 when 'O' then 4 else 1 end)=3 THEN '4'
					WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5                 else 1 end)=4 THEN '5'
				 end role,
				 exam_timeslot.timecode,
				 exam_timeslot.row 
				FROM
					exam_teams_planned
				INNER JOIN exam_timeslot ON exam_teams_planned.col = exam_timeslot.col and exam_teams_planned.exam=exam_timeslot.exam
			) examiner
		
		LEFT OUTER JOIN exam ON examiner.examcode = exam.examcode AND examiner.[day] = exam.[day] AND examiner.timecode = exam.timecode
		LEFT OUTER JOIN exam_cases_planned on exam_cases_planned.examcode=examiner.examcode and exam_cases_planned.day=examiner.day and exam_cases_planned.row=examiner.row
		LEFT OUTER JOIN exam_score on exam.id=exam_score.exam_id and exam_score.role=examiner.role
		WHERE 
			exam.id=@exam_id and  
			isnull(exam_score.examiner,examiner.examiner)=@examiner and
			exam_cases_planned.order_num=@order_num

	END
	RETURN ISNULL(@case_num,'SEQ'+CAST(@order_num as varchar))
END