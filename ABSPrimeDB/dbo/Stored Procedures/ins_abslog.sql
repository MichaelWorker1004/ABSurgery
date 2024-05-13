create proc ins_abslog
  @absuser varchar( 10 ),
  @activity varchar (  40 )
as 
   declare  @logtime char(17)

   select @logtime = convert(varchar(20),getdate(),2) + ' ' + 
                     convert(varchar(20),getdate(),8)
 
   insert abslogs(logtime,absuser,activity) 
	  values(@logtime,@absuser,@activity)
