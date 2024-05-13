create proc ins_exam_last
   @candidate char (  6 ) ,
   @exam exam,
   @type char (  1 ) ,
   @year smallint, 
   @status char (  1 )  ,/* C=completed, R=registered*/
   @area char (  1 )  ,
   @examstaken tinyint ,      /* old times from exam_last */
   @result char (  1 )  ,
   @date smalldatetime   /* extract month and year and build a date */
as  
  if exists (select candidate from exam 
        where candidate = @candidate and
              exam = @exam and
              type = @type and
              year = @year)
   begin

        update exam set 
                area              = @area     ,
                status            = @status   ,       
                result            = @result   ,       
                date              = @date

        where candidate = @candidate and
              exam = @exam and
              type = @type and
              year = @year
   end
   else
   begin
           insert exam (candidate, exam, type, year, area, status,
                     result, date)
           values (@candidate, @exam, @type, @year, @area, @status,
                     @result, @date)
   end

