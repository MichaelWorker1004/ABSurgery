CREATE VIEW dbo.V_QECE_EXAMSTAKEN
AS
SELECT     UserId, candidate, 'W' AS Type, COUNT(*) AS CNT, Year
FROM         dbo.exam
WHERE     (result IS NOT NULL) AND (type = 'W') AND exam = 'G'
GROUP BY UserId, candidate, year
UNION
SELECT     UserId, candidate, 'O' AS Type, COUNT(*) AS CNT, Year
FROM         dbo.exam
WHERE     (result IS NOT NULL) AND (type = 'O') AND exam = 'G'
GROUP BY UserId, candidate, year