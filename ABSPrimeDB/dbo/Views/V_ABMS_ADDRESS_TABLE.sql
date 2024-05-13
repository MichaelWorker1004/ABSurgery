CREATE VIEW V_ABMS_ADDRESS_TABLE
AS
select code,ltrim(rtrim(isnull(title,'') + ' ' + isnull(institution,'') + ' ' + isnull(department,''))) AS Address1,
isnull(street1,'') AS Address2,isnull(street2,'') AS Address3,isnull(street3,'') AS Address4,
isnull(city,'') AS City,isnull(state,'') AS 'State or Province',
'Zip code' = (case isnull(street4,'') when  '' then zip else '' end),
'Foreign Zip Code' = (case isnull(street4,'') when  '' then '' else zip end),
Country = (case isnull(street4,'') when '' then 'USA' else street4 end),
'C' AS 'Address Type',1 AS 'Address Privacy/Suppression','N' AS Undeliverable,'Y' AS 'Receive Mail','' AS Phone,'' AS Fax,
isnull(convert(varchar(10),modified,101),'')  AS 'Update Date',
isnull(StatusNbr,0) StatusNbr,LastVerified
FROM address
where mail = 'M'
