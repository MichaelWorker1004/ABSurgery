
CREATE proc ins_exam
	@candidate char (  6 ) ,
	@exam exam,
	@type char (  1 ) ,
	@year smallint ,
	@status char (  1 )  ,
	@date smalldatetime,
	@center char (  3 )  ,
	@day char (  1 )  ,
	@briefing char (  1 )  ,
	@session char (  1 )  ,
	@number tinyint ,
	@result char (  1 )  ,
	@cardcode char ( 1 ),
	@grp_cod char ( 1 ) ,
	@area char ( 1 ) ,
	@cecode char ( 1),
	@note varchar (  8000 ),
	@attempt tinyint=NULL,
	@rply_sent smalldatetime=null,
	@id numeric
	
as 
	DECLARE @lid numeric
	SET NOCOUNT ON	
	set @lid=0
	If @id>0 
	begin
		update exam set 
			date              = @date           , 
			center            = @center         , 
			day               = @day            , 
			briefing          = @briefing       , 
			session           = @session        , 
			number            = @number         , 
			status            = @status         , 
			result            = @result         , 
			cardcode          = @cardcode       , 
			grp_cod           = @grp_cod        ,
			area              = @area           ,
			note              = @note           ,
			cecode            = @cecode   ,
			year		=@year,
			exam		=@exam,
			type		=@type,
			attempt		=@attempt,
			rply_sent	=@rply_sent
		where id=@id
		set @lid=@id
	end
	else
	begin
		insert exam (candidate, exam, type, date, year, center, number,
			status, result, cardcode, grp_cod, cecode,day,briefing,
			session, area, note,attempt,rply_sent)
		values  (@candidate, @exam, @type, @date, @year, @center, @number,
			@status, @result, @cardcode, @grp_cod, @cecode,@day,@briefing,
			@session, @area, @note,@attempt,@rply_sent)
		set @lid=@@identity
	end
	select @lid