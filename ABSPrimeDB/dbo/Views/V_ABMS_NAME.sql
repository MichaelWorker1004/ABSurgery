CREATE VIEW V_ABMS_NAME
AS
select ABMSUID,Board_Unique_UserID,Board_Unique_ID,SSN,CSSN,
[First Name],[Middle Name],[Last Name],Suffix,
Degree,Gender,[Deceased Indicator],Birthdate,[Medical School Year], Email,[Update Date] FROM 
V_ABMS_SURGEON

