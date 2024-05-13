CREATE FUNCTION udf_isconflict (@examiner char(6),@examinee char(6))
RETURNS smallint
AS
begin
DECLARE 
@result smallint

set @result=0

begin
	if exists(select z.exam from candidate_program z
		inner join candidate_program a on a.program=z.program and a.candidate=@examinee 
		where z.candidate=@examiner)
		begin
			set @result=1
		end
end

begin 
	if exists(SELECT a.id from exam a inner join exam_score b on b.exam_id=a.id and b.examiner=@examiner where a.candidate=@examinee)
	begin
		set  @result=1
	end	
end

begin 

	if exists(SELECT a.candidate from exam_exception a 
		inner join surgeon b on rtrim(b.last_name)=a.examiner
		where a.candidate=@examinee)
	begin
		set  @result=1
	end	
end


return @result
end