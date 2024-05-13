CREATE FUNCTION udf_get_date_weekending (@srcdate as DATETIME)  
RETURNS DATETIME AS  
BEGIN 
DECLARE @basedate AS DATETIME
DECLARE	@result AS DATETIME
SET @basedate = '20010106'

SELECT @result=
  DATEADD(day, weeknum * 7, @basedate) 
FROM (SELECT
        DATEDIFF(day, @basedate, @srcdate + 6) / 7 AS weeknum) AS WN

return @result
END

