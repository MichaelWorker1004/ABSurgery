CREATE proc get_pgmdesc
        @num char(4),
        @exam exam
as
        select exam, name1, name2, abbname, pd, last, l1,l2,l3,l4,l5,l6,total,
        approval, convert( char(8), effdate, 1), booksord, booksent, paid,
        invoice, convert( char(8), pdate, 1), 
        convert( char(8), pdate1, 1), 
        convert( char(8), pdate2, 1), 
        convert( char(8), pdate3, 1), 
        convert( char(8), pdate4, 1), 
        amt, amt1, amt2, amt3, amt4, designee, invsent, po, po1,po2,po3,po4, 
        altname, note,(select fee from fees where exam=@exam and type='i' ) amtperbook,designeename,
        convert( char(8), sdate, 1) ,RRC_ID,RRC_TYPE,[ProgramId] ,junior,senior 
        from program   where number = @num and exam=@exam