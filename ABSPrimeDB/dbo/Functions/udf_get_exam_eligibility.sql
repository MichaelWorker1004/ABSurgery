CREATE FUNCTION udf_get_exam_eligibility (@candidate varchar(6), @examtype varchar(2))
RETURNS varchar(11)
AS
BEGIN
  DECLARE @result varchar(11);
  BEGIN
    select @result='N:XXXX-XXXX';                                                                                                                                                                                                                                                    

    select @result=isnull(ee.admissible, 'N') + ':' + isnull(cast(ee.year_start as varchar), 'XXXX') + '-' + isnull(cast(ee.year_end as varchar), 'XXXX') 
    from exam_eligibility ee
    where ee.candidate=@candidate and ee.exam+ee.type = @examtype

  END

RETURN @result
END