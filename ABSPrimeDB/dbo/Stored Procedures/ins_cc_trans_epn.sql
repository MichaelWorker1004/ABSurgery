CREATE proc ins_cc_trans_epn
	@doc text
as 
	DECLARE @idoc int,
		@ssl_txn_id varchar(46),@ssl_txn_time datetime,@ssl_amount money,@ssl_invoice_number varchar(16)
	
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	SELECT 	@ssl_txn_id=ssl_txn_id,
				@ssl_amount=ssl_amount,
		@ssl_invoice_number=ssl_invoice_number
	FROM OPENXML (@idoc, '/*',10)
	with(	ssl_txn_id		varchar(46) 	'transid',
				ssl_amount		money 		'Total',
		ssl_invoice_number 	varchar(16) 	'ID')
		
	insert into fee_received 	
		(inv_num, 		amount, 	trans_id, 	trans_time,	trans_log) values 
		(@ssl_invoice_number,	@ssl_amount,	@ssl_txn_id,	getdate(),	@doc)			

	EXEC sp_xml_removedocument @idoc