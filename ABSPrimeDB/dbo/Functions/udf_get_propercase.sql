CREATE FUNCTION udf_get_propercase (@par varchar(8000))
RETURNS varchar(8000)
AS
BEGIN
    set @par=ltrim(rtrim(isnull(@par,'')))

    if(len(@par))>1
    begin
        set @par=upper(left(@par,1))+lower(right(@par,len(@par)-1))

        declare @temp int
        set @temp=0
        while charindex(' ',@par,@temp)>0
        begin
            set @par=stuff(@par,charindex(' ',@par,@temp)+1,1,upper(substring(@par,charindex(' ',@par,@temp)+1,1)))
            set @temp=charindex(' ',@par,@temp)+1
        end

        while charindex('  ',@par)>0
        begin
            set @par=replace(@par,'  ',' ')
        end
    end
    else if len(@par)=1
    begin
        set @par=upper(@par)
    end

    return @par
END