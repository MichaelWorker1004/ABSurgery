--drop proc get_address_XML

--drop proc get_address_XML
CREATE proc get_address_XML
	@code as varchar(6)
as
	SET NOCOUNT ON	
	select 
		/*[First Name] FirstName,
		[Last Name] LastName,
		case [Middle Name] when '' then null else [Middle Name] end MiddleName,*/
		dbo.udf_get_address(Board_Unique_id,'address1') q146,
		dbo.udf_get_address(Board_Unique_id,'address2') q147,
		dbo.udf_get_address(Board_Unique_id,'city') q148,
		dbo.udf_get_address(Board_Unique_id,'state') q149,
		dbo.udf_get_address(Board_Unique_id,'zip') q150,
		dbo.udf_get_address(Board_Unique_id,'country') q151
	FROM
		v_abms_surgeon as response 
	WHERE
		Board_unique_id =@code
	for xml auto,elements