CREATE VIEW V_ABMS_ADDRESS
AS
select ABMSUID,Board_Unique_UserID,Board_Unique_ID,SSN,CSSN,
[First Name],[Middle Name],[Last Name],Birthdate,[Medical School Year], 
Address1,Address2,Address3,Address4,City,[State or Province],
[Zip code],[Foreign Zip Code],Country,
[Address Type],[Address Privacy/Suppression],Undeliverable,[Receive Mail],Phone,Fax,V_ABMS_ADDRESS_TABLE.[Update Date] ,
StatusNbr,LastVerified 
FROM V_ABMS_SURGEON
left outer join V_ABMS_ADDRESS_TABLE ON V_ABMS_ADDRESS_TABLE.Code=V_ABMS_SURGEON.Board_Unique_ID


