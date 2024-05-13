CREATE VIEW V_SYSOBJ_TEXT  AS  
	select a.name,z.text,a.xtype, isnull(b.name,'') tr_parent from dbo.syscomments z 
	inner join dbo.sysobjects a on a.id=z.id
	left outer join dbo.sysobjects b on a.parent_obj=b.id
	where a.xtype in ('P','V','FN','TR') and z.colid=1 and a.status >=0