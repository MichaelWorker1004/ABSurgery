CREATE FUNCTION dbo.udf_get_age(
   @FromDate datetime,
   @ToDate datetime
)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN (
   SELECT
      Convert(varchar(11), AgeYears) + 'y '
      + Convert(varchar(11), AgeMonths) + 'm '
      + Convert(varchar(11), DateDiff(day, DateAdd(month, AgeMonths,
         DateAdd(year, AgeYears, @FromDate)), @ToDate)
      ) + 'd' Age
   FROM (
      SELECT
         DateDiff(year, @FromDate, @ToDate)
            - CASE WHEN Month(@FromDate) * 32 + Day(@FromDate)
            > Month(@ToDate) * 32 + Day(@ToDate) THEN 1 ELSE 0 END AgeYears,
         (DateDiff(month, @FromDate, @ToDate)
            - CASE WHEN Day(@FromDate) > Day(@ToDate) THEN 1 ELSE 0 END) % 12 AgeMonths
   ) X
);