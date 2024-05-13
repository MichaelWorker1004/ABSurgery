CREATE VIEW V_ABMS_DECEASED
AS
select ABMSUID,Board_Unique_UserID,Board_Unique_ID,SSN,CSSN,
[First Name],[Middle Name],[Last Name],Suffix,
Degree,[Deceased Indicator],[Deceased Date],Birthdate,[Medical School Year], [Update Date] FROM 
V_ABMS_SURGEON WHERE [Deceased Indicator] = 'Y'

