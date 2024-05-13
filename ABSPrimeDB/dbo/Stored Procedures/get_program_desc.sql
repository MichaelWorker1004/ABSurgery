create proc get_program_desc
   @number char (  4 ) ,
   @exam exam
as  
        select number + exam, abbname from program
        where number=@number and exam=@exam
