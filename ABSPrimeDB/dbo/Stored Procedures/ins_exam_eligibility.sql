create proc ins_exam_eligibility
   @candidate char (  6 ) ,
   @exam exam,
   @type char (  1 ) ,
   @year_start smallint,
   @year_end smallint,
   @examstaken tinyint,
   @admissible char (  1 )

as 

   delete exam_eligibility where 
                candidate = @candidate and
                exam      = @exam and
                type      = @type

   insert exam_eligibility
         (candidate, exam, type, year_start, year_end, examstaken, admissible) 
           values
         (@candidate, @exam, @type, @year_start, @year_end, @examstaken, @admissible) 
