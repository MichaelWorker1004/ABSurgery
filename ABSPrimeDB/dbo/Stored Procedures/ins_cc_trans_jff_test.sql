--drop proc ins_cc_trans_jff_test
CREATE proc ins_cc_trans_jff_test
	@doc text
as 
	DECLARE @idoc int,
		@ssl_txn_id varchar(46),@ssl_txn_time smalldatetime,@ssl_amount money,@ssl_invoice_number varchar(16),
		@cTest char(1),
		@type char(1)
	
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	SELECT 	@ssl_txn_id=ssl_txn_id,
		@ssl_txn_time=ssl_txn_time,
		@ssl_amount=ssl_amount,
		@ssl_invoice_number=ssl_invoice_number
	FROM OPENXML (@idoc, '/*',10)
	with(	ssl_txn_id		varchar(46) 	'ssl_txn_id',
		ssl_txn_time 		smalldatetime 	'ssl_txn_time',
		ssl_amount		money 		'ssl_amount',
		ssl_invoice_number 	varchar(16) 	'ssl_invoice_number')

	
	if substring(@ssl_invoice_number,1,1)='P' 
	begin
		set @type='P'
	end
	else
	begin
		set @type=substring(@ssl_invoice_number,13,1)
		if @type='A' 
		begin
			set @type='T'
		end
	end
	
	EXEC sp_xml_removedocument @idoc

	select 'exec ins_fee_received null,'+
		+@type+','
		+@ssl_invoice_number+','
   		+cast(@ssl_amount as varchar)+','
		+cast(@ssl_txn_time as varchar)+','
		+'null,'
		+@ssl_txn_id+','
		+cast(@doc as varchar)


