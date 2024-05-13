
--drop FUNCTION udf_get_examavail
CREATE FUNCTION udf_get_examavail ( @examcode varchar(7),@over_incl bit)
RETURNS int
AS
BEGIN
declare @result int, @over float

if @over_incl=0 -- (0) actual number of seats available ; (1) margin is added (usually 10%) set in mcodes
begin
	set @over=1
end 
else
begin
	select @over=(cast(attr as numeric)/100)+1 from mcodes where grp='MS' and code='OM'
end

if exists(select * from mcodes where grp='EL' and code=@examcode)
begin
	set @result=0
end
else
begin
	select @result = (select floor(sum(cast(attr2 as int))*@over) from mcodes where code = @examcode and grp='MA') - count(*) from exam where 
		cast(year as varchar(4))+exam+type+rtrim(area)=@examcode and status in ('R','T')
end

return @result
end


