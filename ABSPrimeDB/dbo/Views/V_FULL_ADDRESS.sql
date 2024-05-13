CREATE view V_FULL_ADDRESS AS
select code,type,
(case isnull(ltrim(rtrim(title)),'') when '' then '' else ltrim(rtrim(title))+char(13)+char(10) end) + 
(case isnull(ltrim(rtrim(institution)),'') when '' then '' else ltrim(rtrim(institution))+char(13)+char(10) end) + 
(case isnull(ltrim(rtrim(department)),'') when '' then '' else ltrim(rtrim(department))+char(13)+char(10) end) + 
(case isnull(ltrim(rtrim(street1)),'') when '' then '' else ltrim(rtrim(street1))+char(13)+char(10) end) + 
(case isnull(ltrim(rtrim(street2)),'') when '' then '' else ltrim(rtrim(street2))+char(13)+char(10) end) + 
(case isnull(ltrim(rtrim(street3)),'') when '' then '' else ltrim(rtrim(street3))+char(13)+char(10) end) + 
(case isnull(ltrim(rtrim(city)),'') when '' then '' else ltrim(rtrim(city))+', ' end) + 
(case isnull(ltrim(rtrim(state)),'') when '' then '' else ltrim(rtrim(state))+' ' end) + 
(case isnull(ltrim(rtrim(zip)),'') when '' then '' else ltrim(rtrim(zip))+char(13)+char(10) end) + 
(case isnull(street4,'') when '' then 'USA' else street4 end) as Address
from address where mail='M'