--cdd
--bcp "select * from ABS..V_PEARSON_CDD order by ClientCandidateID desc" queryout "c:\result.txt" /S MIXMASTER -T -c -e"c:\error.txt"
CREATE VIEW dbo.V_PEARSON_CDD_UPDT
AS
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
 isnull( dbo.udf_get_address(Board_Unique_id,'state'),'') State,
 isnull( dbo.udf_get_address(Board_Unique_id,'zip'),'') PostalCode,
 isnull( dbo.udf_get_address(Board_Unique_id,'street3'),'') MailStop,
 isnull( dbo.udf_get_address(Board_Unique_id,'country'),'') Country,
 isnull( dbo.udf_get_phone(Board_Unique_id),'') Phone,
 '' as Extension,
 '' as Fax,
 '' as AddressType,
 '' as FaxCountryCode,
 '1' as PhoneCountryCode
FROM
   v_abms_surgeon as surg

