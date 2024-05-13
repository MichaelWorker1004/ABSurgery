create proc ins_exam_fee_reg
   @candidate char (  6 ) ,
   @exam exam,
   @type char (  1 ) ,
   @year smallint ,
   @rply_sent smalldatetime ,
   @rply_rcvd smalldatetime ,
   @fee_rcvd smalldatetime ,
   @fee_bounce smalldatetime ,
   @adms_sent smalldatetime ,
   @adms_rcvd smalldatetime ,
   @fee_mony_rcvd money  
as 
   if exists (select candidate from exam 
        where candidate = @candidate and
              exam = @exam and
              type = @type and
              year = @year)
   begin

        update exam set 
                rply_sent   =  @rply_sent   ,
                rply_rcvd   =  @rply_rcvd   ,
                fee_rcvd    =  @fee_rcvd    ,
                fee_bounce  =  @fee_bounce  ,
                adms_sent   =  @adms_sent   ,
                adms_rcvd   =  @adms_rcvd   ,
                fee_mony_rcvd       =  @fee_mony_rcvd   

        where candidate = @candidate and
              exam = @exam and
              type = @type and
              year = @year
   end
   else
   begin
        insert exam(candidate,exam,type,year,
                   rply_sent,rply_rcvd,fee_rcvd,fee_bounce,
                   adms_sent,adms_rcvd,fee_mony_rcvd)
        values(@candidate,@exam,@type,@year,
                   @rply_sent,@rply_rcvd,@fee_rcvd,@fee_bounce,
                   @adms_sent,@adms_rcvd,@fee_mony_rcvd)
   end
