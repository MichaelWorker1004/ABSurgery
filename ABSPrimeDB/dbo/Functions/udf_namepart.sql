CREATE FUNCTION udf_namepart(@name varchar(100),@field char(1))
RETURNS varchar(50)
AS
BEGIN
    set @name=REPLACE(@name,'b. sc.','')    
    set @name=REPLACE(@name,'Dr.','')
    set @name=REPLACE(@name,'.','')

    set @name=rtrim(ltrim(@name))

    set @name=REPLACE(@name,', ',',')
    set @name=REPLACE(@name,',MD','')
    set @name=REPLACE(@name,',FACS','')
    set @name=REPLACE(@name,',DO','')
    set @name=REPLACE(@name,',Program Director','')
    set @name=REPLACE(@name,',C-TAGME','')

    set @name=REPLACE(@name,' MD','')
    set @name=REPLACE(@name,' FACS','')
        set @name=REPLACE(@name,' Program Director','')
    set @name=REPLACE(@name,' C-TAGME','')
    

    set @name=REPLACE(@name,' ',' ')
    set @name=REPLACE(@name,' ',' ')

    set @name=rtrim(ltrim(@name))

    if (charindex(',',@name)>0)
    begin
        if (@field='S')
        begin
            set @name=substring(@name,charindex(',',@name)+1,len(@name))
        end
        else
        begin
            set @name=left(@name,charindex(',',@name)-1)
        end
    end
    else if (@field='S')
    begin
        set @name=''
    end
   
    if (@field='F')
    begin
        set @name=left(@name,charindex(' ',@name)-1)
    end 
    else if (@field='L')
    begin
        if ((len(@name)-len(replace(@name,' ','')))=1)
        begin
            set @name=right(@name,len(@name)-charindex(' ',@name))
        end
        else
        begin
            set @name=right(@name,len(@name)-charindex(' ',@name,charindex(' ',@name)+1))
        end
    end 
    else if (@field='M')
    begin
        if (len(@name)-len(replace(@name,' ','')))=1
        begin
            set @name=''
        end
        else
        begin
            set @name=substring(@name,charindex(' ',@name)+1,charindex(' ',@name,charindex(' ',@name)+1)-charindex(' ',@name))
        end
    end

RETURN @name

END