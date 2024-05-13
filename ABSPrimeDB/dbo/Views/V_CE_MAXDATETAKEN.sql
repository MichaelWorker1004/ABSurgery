CREATE VIEW dbo.V_CE_MAXDATETAKEN
AS
SELECT     MAX([date]) AS MaxExamDate, UserId, candidate
FROM         dbo.exam
WHERE     (result IS NOT NULL) AND (type = 'O')
GROUP BY UserId, candidate