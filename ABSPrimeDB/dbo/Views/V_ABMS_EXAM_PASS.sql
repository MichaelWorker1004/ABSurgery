CREATE VIEW V_ABMS_EXAM_PASS
AS
select distinct exam_pass.UserId,
exam_pass.candidate,
--exam.year,exam.area,
exam_pass.type,exam_pass.exam,'Occurence Type'=(case exam_pass.type when 'W' then 'I' when 'O' then 'I' else 'R' end),
'Duration Type'=(case exam_pass.expiration when 2099 then 'L' else 'TL' end),
'Certificate Type'=(case exam_pass.exam when 'G' then 'G' else 'S' end),
'Certification Name'=(case exam_pass.exam when 'G' then 'Surg' when 'C' then 'CritCare' when 'H' then 'Hand' when 'P' then 'Peds' when 'V' then 'Vasc' end) ,
isnull(convert(varchar(10),exam_pass.date,101),'') AS 'Certification Issue',
'Certification Expire' = (case(expiration) when 2099 then 'Indefinite' else cast(isnull(expiration,'') as varchar(4)) end),
isnull(convert(varchar(10),exam_pass.modified,101),'')  AS 'Update Date'
FROM exam_pass
--left outer join exam ON exam.candidate = exam_pass.candidate 
where isnull(expiration,'')!=''



