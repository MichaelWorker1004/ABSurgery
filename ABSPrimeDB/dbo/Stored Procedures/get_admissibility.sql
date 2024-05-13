create proc get_admissibility  
  @candidate char(6) 
as
create table #summary  
  (row tinyint,   
  exam char(1),   
  qe_year_start smallint null,
  qe_year_end smallint null,
  qe_examstaken tinyint null,
  qe_admissible char(1) null,
  ce_year_start smallint null,
  ce_year_end smallint null,
  ce_examstaken tinyint null,
  ce_admissible char(1) null,
  re_year_start smallint null,
  re_year_end smallint null,
  re_examstaken tinyint null,
  re_admissible char(1) null)



insert #summary values (1,'G',null,null,null,null,null,null,null,null,null,null,null,null)
insert #summary values (2,'P',null,null,null,null,null,null,null,null,null,null,null,null)
insert #summary values (3,'V',null,null,null,null,null,null,null,null,null,null,null,null)  
insert #summary values (4,'C',null,null,null,null,null,null,null,null,null,null,null,null)
insert #summary values (5,'H',null,null,null,null,null,null,null,null,null,null,null,null)

update #summary 
set     qe_year_start = year_start, 
        qe_year_end   = year_end,
        qe_examstaken = examstaken,
        qe_admissible = admissible
  from #summary, exam_eligibility
  where candidate = @candidate
    and #summary.exam = exam_eligibility.exam
    and type = 'W'

update #summary 
set     ce_year_start = year_start, 
        ce_year_end   = year_end,
        ce_examstaken = examstaken,
        ce_admissible = admissible
  from #summary, exam_eligibility
  where candidate = @candidate
    and #summary.exam = exam_eligibility.exam
    and type = 'O'

update #summary 
set     re_year_start = year_start, 
        re_year_end   = year_end,
        re_examstaken = examstaken,
        re_admissible = admissible
  from #summary, exam_eligibility
  where candidate = @candidate
    and #summary.exam = exam_eligibility.exam
    and type = 'R'

select row, exam, qe_year_start, qe_year_end, qe_examstaken,qe_admissible,
                  ce_year_start, ce_year_end, ce_examstaken,ce_admissible,
                  re_year_start, re_year_end, re_examstaken,re_admissible
from #summary
order by row
