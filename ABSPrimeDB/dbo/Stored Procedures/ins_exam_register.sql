create proc ins_exam_register
   @candidate char (  6 ) ,
   @exam exam,
   @type char (  1 ) ,
   @year smallint ,
   @status char (  1 )  , /* C=completed, R=registered*/
   @date smalldatetime,
   @center char (  3 )  ,
   @area char ( 1 ) ,
   @number tinyint
as 
   if exists (select candidate from exam 
        where candidate = @candidate and
              exam = @exam and
              type = @type and
              year = @year)
   begin

        update exam set 
                center            = @center         , 
                area              = @area           , 
                number            = @number         , 
                date              = @date           , 
                status            = @status

        where candidate = @candidate and
              exam = @exam and
              type = @type and
              year = @year
   end
   else
   begin
           insert exam (candidate, exam, type, year, center, area, number, status, date)
           values      (@candidate, @exam, @type, @year, @center, @area, @number, @status,@date)
   end
