CREATE FUNCTION udf_get_examcounts ( @candidate char(6), @exam char(1), @type char(1))
RETURNS varchar(6)
AS
BEGIN
declare @result varchar(6)
select @result = (select cast(count(*) as varchar(1))+' - '+cast(max(result) as varchar(1)) as examcounts
FROM 
	exam ex 
LEFT OUTER JOIN 
	exam_pass exp ON ex.candidate=exp.candidate and ex.exam=exp.exam and ex.type=exp.type 
WHERE
	ex.candidate = @candidate AND
	ex.exam = @exam and
	ex.type = @type and
	ex.result <> ''
GROUP BY 
 	ex.exam,
	ex.type)

return @result
end

