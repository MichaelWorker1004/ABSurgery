CREATE proc get_pearson_cdd
	@date smalldatetime
as 
set nocount on
select 
 '' CandidateID, 
 'ABS0'+surg.Board_Unique_ID ClientCandidateID, 
 surg.[First Name] FirstName,
 surg.[Last Name] LastName,
 case surg.[Middle Name] when '' then '' else surg.[Middle Name] end MiddleName,
 'Dr.' as Salutation,
 case surg.email when '' then '' else surg.email end Email,
 '' UserName,
 '' [Password],
 replace(convert(varchar(10),cast(surg.[Update Date] as smalldatetime), 121),'-','/') LastUpdate,
 isnull(dbo.udf_get_address(Board_Unique_id,'address1'),'') Address1,
 isnull(dbo.udf_get_address(Board_Unique_id,'address2'),'') Address2,
 isnull( dbo.udf_get_address(Board_Unique_id,'address3'),'') Address3,
 isnull( dbo.udf_get_address(Board_Unique_id,'city'),'') City,
 IIF(isnull( dbo.udf_get_address(Board_Unique_id,'state'),'')='PR', '', isnull( dbo.udf_get_address(Board_Unique_id,'state'),'')) State,
 isnull( dbo.udf_get_address(Board_Unique_id,'zip'),'') PostalCode,
 isnull( dbo.udf_get_address(Board_Unique_id,'street3'),'') MailStop,
 IIF(isnull( dbo.udf_get_address(Board_Unique_id,'state'),'')='PR', 'PRI', isnull( dbo.udf_get_address(Board_Unique_id,'country'),''))Country,
 isnull( dbo.udf_get_phone(Board_Unique_id),'') Phone,
 '' as Extension,
 '' as Fax,
 '' as AddressType,
 '' as FaxCountryCode,
 '1' as PhoneCountryCode
FROM
   v_abms_surgeon as surg 
WHERE (surg.Board_Unique_ID in
	(select candidate from exam where pearson_sent= @date 
			))
	or (cast( dbo.udf_get_address(Board_Unique_id,'modified') as smalldatetime)>(select attr from mcodes where code='PV' and grp='MS') and 
			Board_Unique_id in (select distinct candidate from exam where pearson_sent is not null or pearson_auth_id is not null))

update mcodes set attr=getdate() where code='PV' and grp='MS'