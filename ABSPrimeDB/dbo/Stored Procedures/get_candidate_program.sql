create proc get_candidate_program
   @candidate char (  6 )
as  
        select convert(char(4),compyear), exam, program, remedial
        from candidate_program where  candidate = @candidate 
