create proc del_exam_eligibility
   @candidate char (  6 ) ,
   @exam exam,
   @type char (  1 ) 
as 
   delete exam_eligibility 
       where candidate = @candidate and
              exam = @exam and
              type = @type

