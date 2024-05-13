CREATE PROCEDURE [dbo].[get_residency_program]
AS
	SELECT 
		p.ProgramId, isnull(pa.[STATE], '') + ' - ' + abbname + ' [' + p.number + ']' as ProgramName
	FROM 
		program p 
		left join program_addresses pa on pa.ProgramId=p.ProgramId
	where 
		rtype = 0
		and approval in ('a','c') 
	order by 
		abbname asc
