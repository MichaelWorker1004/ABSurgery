CREATE PROCEDURE [dbo].[get_accredited_program_institutions]
AS
	SELECT 
		p.ProgramId,
		dbo.udf_get_institutionname(p.ProgramId) as InstitutionName,
		pa.City,
		pa.STATE as State
	FROM
		program p
		left join program_addresses pa on p.ProgramId = pa.ProgramId
	WHERE
		p.approval in ('A', 'C')
	ORDER BY 
		p.abbname

RETURN 0
