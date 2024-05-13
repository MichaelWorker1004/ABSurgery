CREATE proc ins_relation
	@ParentID 	numeric,
   	@number 	char(4),
   	@relationship 	char(1)=null,
	@deleted	bit=0
as 
	If @deleted=1 
	begin
		update relations set date_deleted = getdate()
		where parentid = @ParentID and ChildID in (SELECT ProgramId FROM Program WHERE number=@number) 
	end
	else
	begin
		update relations set  
		        relationship = @relationship, 
		        date_deleted      = null 
		where parentid = @ParentID and ChildID in (SELECT ProgramId FROM Program WHERE number=@number) 

		insert relations (parentid,childid,relationship) 
			select @ParentID,ProgramId,@relationship from Program 
			where number=@number and ProgramId not in (
				select ProgramId from relations where parentid=@ParentID and ChildID in 
					(SELECT ProgramId FROM Program WHERE number=@number)) 
	end