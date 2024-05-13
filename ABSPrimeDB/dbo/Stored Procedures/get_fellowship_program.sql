CREATE PROCEDURE [dbo].[get_fellowship_program]
@FellowshipType VARCHAR(1)
AS
	SELECT 
		p.ProgramId, 
		isnull(pa.[STATE], '') + ' - ' + abbname + ' [' + p.number +p.exam+ ']' as ProgramName
	FROM
		program p 
		left join program_addresses pa on pa.ProgramId=p.ProgramId
	where 
		rtype = 1
		and approval in ('a','c')
		AND p.exam = @FellowshipType
	order by 
		abbname asc