CREATE proc ins_candidate_program
   @candidate char (  6 ) ,
   @exam exam,
   @compyear smallint ,
   @program char (  4 ),
   @pass_fail char ( 1 ),
   @note varchar (  255 ),
   @remedial char ( 1 )
as 
   if exists (select candidate from candidate_program 
        where candidate = @candidate and
              exam = @exam and
              compyear = @compyear and
              program = @program)
   begin

        update candidate_program set 
                pass_fail         = @pass_fail      , 
                remedial          = @remedial       , 
                note              = @note   

        where candidate = @candidate and
              exam = UPPER(@exam) and
              compyear = @compyear and
              program = @program
   end
   else
   begin
        insert candidate_program
           (candidate,exam,compyear,program,pass_fail,remedial,note)
           values
           (@candidate,UPPER(@exam),@compyear,@program,@pass_fail,@remedial,@note)
   end