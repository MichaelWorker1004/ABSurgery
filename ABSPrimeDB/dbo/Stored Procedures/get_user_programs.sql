CREATE PROCEDURE [dbo].[get_user_programs]
	@UserId int
AS
	SELECT TOP (1000) 
      ProgramName = [abbname]
      ,ProgramDirector = [pd]
      ,ProgramNumber = p.[number]
      ,pa.City
      ,pa.State
      ,Exam = p.[exam]
      ,ClinicalLevel =
      case 
        when re.[level] in (1, 2, 3, 4, 5) and p.rtype = 0 then 'PGY' + convert(char(1), re.[level])
        when re.[level] in (1, 2) and p.rtype = 1 then 'Fellow Clinical Level ' + convert(char(1), re.[level])
        when re.[level] = 8 then 'Research' 
        when re.[level] = 9 then 'Other' 
      end
  FROM [dbo].[program] p
  left join [dbo].[roster] r  on p.number = r.number and p.exam = r.exam and r.current_year = Year(GETDATE())
  left join [dbo].[resident] re on r.id = re.roster_id
  left join [dbo].[user_profiles] up on up.ABSId = re.candidate
  left join [dbo].[program_addresses] pa on p.ProgramId = pa.ProgramId
  where 
    up.UserId = @UserId