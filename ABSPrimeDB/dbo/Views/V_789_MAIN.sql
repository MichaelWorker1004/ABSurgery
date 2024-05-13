CREATE  VIEW dbo.V_789_MAIN
AS
select
      exam_pass.candidate,
      exam_pass.exam,
      exam_pass.expiration,
      dbo.udf_get_namelast(exam_pass.candidate) namelast,
      case isnull(address.zip, '') when '' then exam_pass.candidate + 'S' else exam_pass.candidate end addresslookup,
      case isnull(address.zip, '') when '' then 'ABMS' else 'ABS' end addresstype,
      case isnull(address.zip, '') when '' then dbo.udf_get_addressblock(exam_pass.candidate +'S') else dbo.udf_get_addressblock(exam_pass.candidate) end addressblock,
      case isnull(surgeon_st.candidate,'') when '' then '' else 'Y' end director 
  from
       (select candidate, exam, max(expiration) expiration from exam_pass group by candidate, exam) as exam_pass 
	left outer join surgeon_st on surgeon_st.candidate=exam_pass.candidate and status_code='AM'
	left outer join dscpl_action ON dscpl_action.candidate=exam_pass.candidate and dscpl_action.effective = 1
	left outer join
       exam_eligibility on exam_pass.candidate = exam_eligibility.candidate and exam_pass.exam = exam_eligibility.exam and exam_eligibility.type = 'R' left outer join
       address on exam_pass.candidate = address.code and address.status='S' and address.mail='M'
  where
       isnull(exam_eligibility.admissible, '') <> 'Y' and
       dscpl_action.candidate is null and 
       exam_pass.candidate not like '09%'
and exam_pass.candidate not in (select candidate from surgeon_st where status_code in ('NM','DE','CI'))



