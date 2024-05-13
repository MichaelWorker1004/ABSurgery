create proc get_exam_eligibility
   @candidate char (  6 ),
   @exam exam,
   @type char (  1 )
as 
   select year_start, year_end, examstaken,admissible from exam_eligibility
   where
     candidate  = @candidate and
     exam       = @exam and
     type       = @type
