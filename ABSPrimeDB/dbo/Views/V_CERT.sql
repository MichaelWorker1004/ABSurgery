CREATE view [dbo].[V_CERT]
as
select 
   surgeon.UserId,
	surgeon.candidate,
	isnull(info.exam,'') exam,
	isnull(info.type,'') type,
	isnull(info.start_date,'') start_date,
	isnull(info.end_date,'') end_date,
	isnull(info.duration,'') duration,
	isnull(info.Occurrence,'') + case when info.status='Expired' then ' - Expired' when isnull(info.Occurrence,'')='' then info.status else '' end  Occurrence,
	isnull(info.status,'') status, 
	isnull(certificate,'') certificate, 
	isnull(admissible, 'N') admissible,
    isnull(mcodes.Descr,'') revoked,
	case 
		when isnull(cohort,0)=0 or retired.UserId is not null then 'Not Required to Participate in CC' 
		when isnull(num_cycles_non_compliant,0)!=0 then 'Not Meeting CC Requirements' 
		when optout > '2005-01-01' then 'Not Meeting CC Requirements'
		else 'Meeting CC Requirements' 
	end MOCParticipation,
	case isnull(inactive.UserId,'')
		when '' then ''
		else 'true'
	end clinically_inactive,
	case isnull(retired.UserId,'')
		when '' then ''
		else 'true'
	end retired		
from surgeon 
left outer join (
		select case isnull(b.UserId,'') when '' then exam_pass.UserId else b.UserId end UserId, 
         case isnull(b.candidate,'') when '' then exam_pass.candidate else b.candidate end candidate,
			case isnull(b.exam,'') when '' then exam_pass.exam else b.exam end exam,
			case isnull(b.type,'') when '' then exam_pass.type else b.type end type,
			case 
				when year(isnull(date,''))= 1900 then '' 
				when isnull(expiration,'')='' then ''
				else isnull(convert(varchar(10),date,120),'') end start_date,
			case 
				when expiration = 2099 then 'Indefinite' 
				when expiration < 2014 then cast(expiration as varchar)+'-07-01' 
				else cast(expiration as varchar)+'-12-31' 
			end end_date,
			case isnull(expiration,'') when 2099 then 'Lifetime' when '' then '' else 'Time-Limited' end duration,
			case isnull(exam_pass.type,'') when 'O' then 'Initial Certification' when 'W' then 'Initial Certification' when '' then '' else 'Recertification' end Occurrence,
			case 
				when sign(datediff(d,getdate(),cast('12/31/'+cast(expiration as varchar) as datetime))) = -1 then 'Expired' 
				when isnull(expiration,'') = ''  then 'In Examination Process' else 'Active' 
			end status,	
		    isnull(certificate, '') certificate,
		    b.admissible
		from exam_pass 
			full outer join (select UserId, candidate,exam,type,admissible from exam_eligibility where admissible='Y'  and type not in ('X','P')) b on CAST(b.UserId AS varchar)+b.exam+b.type=CAST(exam_pass.UserId AS varchar)+exam_pass.exam+exam_pass.type  
		where (isnull(cast(expiration as varchar(4)),'')+isnull(b.UserId,''))!=''  
	) info on surgeon.UserId=info.UserId
left join surgeon_st inactive on inactive.UserId=surgeon.UserId and inactive.status_code='CI'
left join surgeon_st retired on retired.UserId=surgeon.UserId and retired.status_code='NP'
left join dscpl_action on dscpl_action.UserId=surgeon.UserId and dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR', 'CS') 
left join mcodes on dscpl_action.dscpl_code=mcodes.code and mcodes.grp='DC' 
left join moc_eligibility on moc_eligibility.UserId=surgeon.UserId

UNION

select  exam_pass.UserId,
   exam_pass.candidate,
	exam_pass.exam,
	'',
	convert(varchar(10),act_executed,120) start_date,
	'' end_date,
	'' duration,
	'' Occurrence,
	descr  status,
    '' certificate,
    'N' admissible,
    descr,
    '',
    '',
    ''
from exam_pass 
inner join dscpl_action on dscpl_action.UserId=exam_pass.UserId and dscpl_action.effective=1 and dscpl_action.dscpl_code in ('CR', 'CS')
inner join mcodes on dscpl_action.dscpl_code=mcodes.code and mcodes.grp='DC' 
where exam+type not in ('GW','PW','VW') and type not in ('X','P')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[3] 2[49] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_CERT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_CERT';

