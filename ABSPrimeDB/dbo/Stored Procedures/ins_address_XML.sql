--drop proc ins_address_XML
CREATE proc [dbo].[ins_address_XML]
	@doc text
as 
	DECLARE @idoc int,@query varchar(11),@type char(1),@code varchar(6)
	
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	select @type=type,@code= address.Code from address,
			(SELECT Code FROM OPENXML (@idoc, '//Code',6) with (Code varchar(6))) info 
		   where address.code=info.code and address.mail='M'	

	--address
	if (@type is null) 
	begin
		UPDATE address SET 
			title		=info.title,
			institution	=info.institution,
			street1		=info.street1,
			street2		=info.street2,
			city		=info.city,
			state		=info.state,
			zip		=info.zip
		FROM (SELECT title,institution,street1,street2,city,state,zip,number,exam FROM OPENXML (@idoc, '/program',10) 
		with (	pd 		varchar(42),
			title		varchar(50),
			institution	varchar(50),
			street1		varchar(100),
			street2		varchar(100),
			city		varchar(30),
			state		char(2),
			zip		char(10),
			number 		char(4),
			exam 		exam)) info
		WHERE address.code=info.number+info.exam and address.status='P' and address.mail='M'
	end
	else
	begin
		INSERT address (title,institution,street1,street2,city,state,zip,code,status,mail)
		SELECT title,institution,street1,street2,city,state,zip,number+exam,'P','M' FROM OPENXML (@idoc, '/program',10) 
		with (	pd 		varchar(40),
			title		varchar(50),
			institution	varchar(50),
			street1		varchar(100),
			street2		varchar(100),
			city		varchar(30),
			state		char(2),
			zip		char(10),
			number 		char(4),
			exam 		exam)
	end
	--phone	
	if exists (select 0 from phone,(SELECT number,exam FROM OPENXML (@idoc, '/program',10) with (number char(4),exam exam)) info where phone.code=info.number+info.exam and phone.status='P' and phone.type1='O' and phone.type2='V') 
	begin
		UPDATE phone SET number=info.phone
		FROM (SELECT phone,number,exam FROM OPENXML (@idoc, '/program',10) with (phone varchar(100),number char(4),exam exam)) info
		WHERE phone.code=info.number+info.exam and phone.status='P' and phone.type1='O' and phone.type2='V'
	end
	else
	begin
		INSERT phone (number,code,status,type1,type2)
		SELECT phone,number+exam,'P','O','V' FROM OPENXML (@idoc, '/program',10) with (phone varchar(100),number char(4),exam exam)
	end
	
	EXEC sp_xml_removedocument @idoc
	
	--EXEC ('get_address_XML' + @query)