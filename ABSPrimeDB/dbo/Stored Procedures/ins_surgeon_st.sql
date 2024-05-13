CREATE proc ins_surgeon_st
   @candidate char (  6 ) ,
   @status_code varchar ( 50 ) ,
   @start_date datetime =null,
   @end_date datetime =null,
   @note varchar (1000) =null,
   @st_val varchar(8000) =null
   
as  
   
   if exists (select id from surgeon_st
              where candidate = @candidate and
              status_code = @status_code)
   begin
       update surgeon_st set
               start_date=@start_date,
               end_date=@end_date,
               note=@note,
               st_val=@st_val,
               modified=getdate(),
	  modifications=modifications+1
       where candidate = @candidate and
              status_code = @status_code
   end
   else
   begin          
         insert into surgeon_st (candidate,status_code,start_date,end_date,note,st_val,modifications,creator,created)
         values (@candidate,@status_code,@start_date,@end_date,@note,@st_val,0,'sp',getdate())
   end
   
   if @status_code='citizenshipcountry'
   begin
   		UPDATE surgeon SET citizen='Y' WHERE candidate=@candidate AND ISNULL(citizen,'')!='Y';
   end