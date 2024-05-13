CREATE VIEW dbo.V_ABMS_SURGEON
AS
SELECT  RTRIM(ISNULL(dbo.ABMSdata.ABMScode, '')) AS ABMSUID,
      a.UserId AS Board_Unique_UserID,
		a.candidate AS Board_Unique_ID,
		RTRIM(ISNULL(a.ssn, '')) AS SSN,
		'' AS CSSN,
		RTRIM(ISNULL(a.npi, '')) AS npi,
		LTRIM(RTRIM(a.first_name)) AS [First Name],
		LTRIM(RTRIM(ISNULL(a.middle_name, ''))) AS [Middle Name],
		LTRIM(RTRIM(a.last_name)) AS [Last Name], 
		LTRIM(ISNULL(CASE ltrim(ISNULL(a.suffix, '')) WHEN '' THEN '' ELSE RIGHT(ltrim(ISNULL(a.suffix, '')),
		LEN(ltrim(ISNULL(a.suffix, ''))) - CHARINDEX(',', 
		ltrim(ISNULL(a.suffix, '')))) END, '')) AS Suffix, 
		(CASE a.degree WHEN 'O' THEN 'DO' WHEN 'M' THEN 'MD' WHEN 'B' THEN 'MBBS' WHEN 'C' THEN 'MBChB' ELSE '' END) AS Degree,
		ISNULL(a.sex, '') AS Gender, 
		(CASE isnull(surgeon_st.candidate, 'N') WHEN 'N' THEN 'N' ELSE 'Y' END) AS [Deceased Indicator], 
		ISNULL(CONVERT(varchar(10), dbo.surgeon_st.start_date, 101), '') AS [Deceased Date],
		CONVERT(varchar(10),ISNULL(ISNULL(ISNULL(dbo.surgeon_st.modified,dbo.surgeon_st.created),a.modified),a.created),120) AS PhysicalStatusLastUpdate,
		ISNULL(CONVERT(varchar(10), a.birthdate, 101), '') AS Birthdate,
		CASE isnull(CAST(a.year AS varchar(4)), '') 
		  WHEN '0' THEN '' ELSE isnull(CAST(a.year AS varchar(4)), '') END AS [Medical School Year],
		ISNULL(dbo.udf_get_email(a.candidate), '') AS Email, 
		ISNULL(CONVERT(varchar(10), a.modified, 101), '') AS [Update Date],
		a.ID ID
FROM	dbo.surgeon AS a 
		LEFT OUTER JOIN dbo.ABMSdata ON dbo.ABMSdata.ABScode = a.candidate 
    	LEFT OUTER JOIN dbo.surgeon_st ON dbo.surgeon_st.candidate = a.candidate AND dbo.surgeon_st.status_code = 'DE'
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ABMSdata"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 114
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "surgeon_st"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_ABMS_SURGEON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_ABMS_SURGEON';

