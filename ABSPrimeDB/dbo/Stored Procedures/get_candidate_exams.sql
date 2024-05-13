CREATE proc get_candidate_exams
   @candidate char (  6 )
as  
        select convert(char(4),year), exam, type, area,attempt,id 
        from exam where  candidate = @candidate order by 
        case status when 'T' then 0 when 'R' then 0 when 'W' then 0 else 1 end,
        year desc,
        exam asc,
        type asc,
        area desc,
        attempt