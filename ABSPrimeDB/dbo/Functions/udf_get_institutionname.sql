CREATE FUNCTION [dbo].[udf_get_institutionname] 
(
	@ProgramId int
)
RETURNS varchar(150)
AS
BEGIN
DECLARE @result varchar(150)

		SELECT @result = 
		(
			SELECT
			 CASE WHEN p.Exam = 'G' THEN p.abbname + ' (General Surgery)'
			  WHEN p.Exam = 'V' THEN p.abbname + ' (Vascular Surgery)'
			  WHEN p.Exam = 'P' THEN p.abbname + ' (Pediatric Surgery)'
			  WHEN p.Exam = 'H' THEN p.abbname + ' (Hand Surgery)'
			  WHEN p.Exam = 'O' THEN p.abbname + ' (CGSO)'
			  WHEN p.Exam = 'C' THEN p.abbname + ' (Surgical Critical Care)'
			  ELSE null
			END 
			FROM
			 program p 
			WHERE 
			 p.ProgramId = @ProgramId 
		)
	RETURN isnull(@result, '')
END
GO