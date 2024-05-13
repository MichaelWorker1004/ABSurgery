CREATE proc get_cand_exam
   @candidate char (  6 ),
   @exam exam,
   @type char (  1 ) ,
   @year smallint,
   @area char (  1 ) ,
   @attempt smallint=NULL
as  
   select convert( char(8), date,       1), convert( char(8), rply_sent, 1), 
          convert( char(8), rply_rcvd,  1), convert( char(8), fee_rcvd , 1), 
          convert( char(8), fee_bounce, 1), convert( char(8), adms_sent, 1), 
          number, status, cardcode, grp_cod,
          area, center, day, briefing, session, result, cecode, note,fee_mony_rcvd
   from exam
   where candidate = @candidate and
         exam      = @exam and   
         type      = @type and   
         area      = @area and   
         year      = @year and   
         isnull(attempt,0) = isnull(@attempt,0)