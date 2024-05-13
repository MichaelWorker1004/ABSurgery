CREATE VIEW V_STAT_CONTAIN_OBJECT  AS  
select z.name Object,z.xtype ObjectType,
	SUBSTRING(a.text,CHARINDEX(z.name,a.text)-1,len(z.name)+2) FirstOccurenceText,
	CHARINDEX(z.name,a.text,CHARINDEX(z.name,a.text)+1) IsThereMore,
	a.name Contains_In,
	a.text,
	a.xtype Container_Type

from V_SYSOBJ_TEXT z 
left outer join V_SYSOBJ_TEXT a on a.text like '%'+z.name+'%' and a.name!=z.name