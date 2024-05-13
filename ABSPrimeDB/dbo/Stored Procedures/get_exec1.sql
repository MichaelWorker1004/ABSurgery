
/****** Object:  Stored Procedure dbo.get_exec1    Script Date: 5/11/01 2:16:24 PM ******/
create proc get_exec1
  @dt   smalldatetime,    
  @yr smallint
as  
BEGIN
   select 'Preliminary Sent:            ', count(*) from track where pre_send>=@dt  and exam='G' and type='Q' and year = @yr
   select 'Preliminary Received:        ', count(*) from track where pre_receive>=@dt and exam='G' and type='Q' and year = @yr
   select 'Preliminary Approved:        ', count(*) from track where pre_approve>=@dt and exam='G' and type='Q' and year = @yr
END

