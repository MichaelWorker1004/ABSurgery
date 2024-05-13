create proc get_admissible
   @candidate char (  6 ),
   @exam exam,
   @type char (  1 )
as 
   select admissible from exam_eligibility
   where
     candidate  = @candidate and
     exam       = @exam and
     type       = @type
