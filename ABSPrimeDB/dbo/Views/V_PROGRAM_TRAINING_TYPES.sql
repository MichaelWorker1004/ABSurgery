create view V_PROGRAM_TRAINING_TYPES
AS
select mcodes.attr rtype,mcodes.code rtype_code, mcodes.descr rtype_descr,
mcodes.attr2 rtype_attr2,m.attr level, m.code level_code, m.descr level_descr
from mcodes 
inner join mcodes m on m.grp=mcodes.code
where mcodes.code like 'R%' and mcodes.grp='MS' and mcodes.attr is not null