CREATE VIEW dbo.V_EXAMINERS
AS
SELECT
	 exam_teams_planned.exam,
	 exam_teams_planned.examcode,
	 exam_teams_planned.[day],
	 exam_teams_planned.board_examiner_userid examiner_userid,
	 exam_teams_planned.board_examiner examiner,
	 'B'+case
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 3 end)=0 THEN '1'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 3 end)=1 THEN '2'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 3 end)=2 THEN '3'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 1 end)=3 THEN '4'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 1 end)=4 THEN '5'
	 end role,
	 exam_teams_planned.team,
	 exam_teams_planned.grp,
	 exam_teams_planned.col,
	 exam_timeslot.timecode,
	 exam_timeslot.row,
	 exam_timeslot.briefing,
	 exam_timeslot.[session]
	FROM
	 exam_teams_planned
	INNER JOIN exam_timeslot ON exam_teams_planned.col = exam_timeslot.col and exam_teams_planned.exam=exam_timeslot.exam
UNION
	SELECT
	 exam_teams_planned.exam,
	 exam_teams_planned.examcode,
	 exam_teams_planned.[day],
	 exam_teams_planned.assoc_examiner_userid examiner_userid,
	 exam_teams_planned.assoc_examiner examiner,
	 'A'+case
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 3 end)=0 THEN '1'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 3 end)=1 THEN '2'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 3 end)=2 THEN '3'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 1 end)=3 THEN '4'
		WHEN (exam_timeslot.row-1) % (case exam_teams_planned.exam when 'P' then 5 else 1 end)=4 THEN '5'
	 end role,
	 exam_teams_planned.team,
	 exam_teams_planned.grp,
	 exam_teams_planned.col,
	 exam_timeslot.timecode,
	 exam_timeslot.row,
	 exam_timeslot.briefing,
	 exam_timeslot.[session]
	FROM
	 exam_teams_planned
	INNER JOIN exam_timeslot ON exam_teams_planned.col = exam_timeslot.col and exam_teams_planned.exam=exam_timeslot.exam