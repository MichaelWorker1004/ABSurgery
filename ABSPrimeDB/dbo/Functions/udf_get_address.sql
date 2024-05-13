CREATE FUNCTION udf_get_address(@code varchar(6),@field varchar(15))
RETURNS varchar(204)
AS
BEGIN
	declare @result varchar(204),
		@org1 varchar(101),@org2 varchar(101),
		@street1 varchar(100),@street2 varchar(100),
		@city varchar(50),@state varchar(50),@zip varchar(50),@country varchar(50),
		@address1 varchar(204),@address2 varchar(202),@address3 varchar(100),
		@type varchar(11),@modified varchar(10),
		@validitydate varchar(10),
		@id varchar(10)
	
	SELECT	@org1=isnull(title,''),@org2=ltrim(rtrim(isnull(institution,''))),
		@street1=isnull(street1,''),@street2=isnull(street2,''),
		@city=rtrim(isnull(city,'')),@state=isnull(state,''),
		@zip=(case SUBSTRING (isnull(zip,''),6,1) when  '-' then left(zip,5) else isnull(zip,'') end),
		@country=(case isnull(street4,'') when '' then 'USA' else street4 end),
		@type=(case type when 'H' then 'Residential' else 'Business' end ),
		@modified=isnull(convert(varchar(10),modified,20),''), 
		@validitydate=dbo.udf_get_lastupdated('address','address',id),
		@id=cast(id as varchar(10))
	FROM address 
	WHERE mail='M' AND code=@code
		
	
	if (len(@street1)=0)
	begin
		set @street1=@street2
		set @street2=''
	end
	
	if (len(@org1)=0)
	begin
		set @org1=@org2
		set @org2=''
	end
	
	if (len(@org1)!=0 and len(@org2)!=0 and len(@street1)!=0 and len(@street2)!=0)
	begin
		set @address1=@org1+', '+@org2
		set @address2=@street1
		set @address3=@street2
	end
	else
	begin
		if (len(@org1)!=0)
		begin
			set @address1=@org1
			set @address2=@street1
			set @address3=@street2
		end
		else
		begin
			set @address1=@street1
			set @address2=@street2
			set @address3=''
		end
	end
	
	select @result= case @field
				when 'org1' then @org1
				when 'org2' then @org2
				when 'street1' then @street1
				when 'street2' then @street2
				when 'street3' then ''
				when 'city' then @city
				when 'state' then @state
				when 'zip' then @zip
				when 'country' then @country
				when 'address1' then @address1
				when 'address2' then @address2
				when 'address3' then @address3
				when 'type' then @type
				when 'modified' then @modified
				when 'validitydate' then @validitydate
				when 'id' then @id
				else 'invalid qualifier'
			end
	
	return  rtrim(isnull(@result,''))

END