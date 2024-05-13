CREATE proc ins_track
   @trackno		char (6),
   @candidate		char (6),
   @exam		exam,
   @type		char (1),
   @year		smallint,
   @pre_send		smalldatetime,
   @pre_receive 		smalldatetime,
   @pre_approve 	smalldatetime,
   @pre_disapprove 	smalldatetime,
   @pre_defer		smalldatetime,
   @pre_credential	smalldatetime,
   @app_send		smalldatetime,
   @app_receive		smalldatetime,
   @app_approve	smalldatetime,
   @app_disapprove	smalldatetime,
   @app_defer 		smalldatetime,
   @app_credential 	smalldatetime,
   @oper_receive	smalldatetime,
   @oper_approve 	smalldatetime,
   @card_receive 	smalldatetime,
   @lic_receive 		smalldatetime,
   @reg_receive 		smalldatetime,
   @let_send 		smalldatetime,
   @let_receive 		smalldatetime,
   @fee_amount 		money,
   @fee_receive 		smalldatetime,
   @fee_bounce 	smalldatetime,
   @program 		char (4),
   @_group 		char (1),
   @compyear 		smallint, 
   @note 		varchar (8000),
   @sig_receive 		smalldatetime,
   @cme_receive	smalldatetime,
   @web_note		varchar(8000)
as 
	if exists (select trackno from track 
		where trackno = @trackno and
		      exam = @exam and
		      type = @type and
		      year = @year  )
	begin
	
	        update track set 
			pre_send	=@pre_send,
			pre_receive	=@pre_receive,	
		        	pre_approve	=@pre_approve,
			pre_disapprove	=@pre_disapprove,
			pre_defer	=@pre_defer,
			pre_credential	=@pre_credential,
		        	app_send	=@app_send,
			app_receive	=@app_receive,
			app_approve	=@app_approve,
			app_disapprove	=@app_disapprove,
		        	app_defer	=@app_defer,
			app_credential	=@app_credential,
			oper_receive	=@oper_receive,
			oper_approve	=@oper_approve,
		       	card_receive	=@card_receive,
			lic_receive	=@lic_receive,
			reg_receive	=@reg_receive,
			let_send	=@let_send,
		       	let_receive	=@let_receive,
			fee_amount	=@fee_amount,
			fee_receive	=@fee_receive,
			fee_bounce	=@fee_bounce,
		       	 program	=@program,
			_group		=@_group,
			compyear	=@compyear,
			note		=@note,
			modified		=convert(datetime,getdate(),101),
   			sig_receive 	=@sig_receive,
   			cme_receive	=@cme_receive,
			web_note	=@web_note
	        	where exam = @exam and
			type = @type and
			year = @year and
			trackno = @trackno
	end
	else
	begin
	
		insert track (trackno, candidate, exam, type, year, pre_send, pre_receive,
		        pre_approve, pre_disapprove, pre_defer, pre_credential,
		        app_send, app_receive, app_approve, app_disapprove,
		        app_defer, app_credential, oper_receive, oper_approve,
		        card_receive, lic_receive, reg_receive, let_send,
		        let_receive, fee_amount, fee_receive, fee_bounce,
		        program, _group, compyear, note,created,
		        sig_receive,cme_receive,web_note)
		values (@trackno, @candidate, @exam, @type, @year, @pre_send, @pre_receive,
		        @pre_approve, @pre_disapprove, @pre_defer, @pre_credential,
		        @app_send, @app_receive, @app_approve, @app_disapprove,
		        @app_defer, @app_credential, @oper_receive, @oper_approve,
		        @card_receive, @lic_receive, @reg_receive, @let_send,
		        @let_receive, @fee_amount, @fee_receive, @fee_bounce,
		        @program, @_group, @compyear, @note,convert(datetime,getdate(),101),
		        @sig_receive,@cme_receive,@web_note) 
	end