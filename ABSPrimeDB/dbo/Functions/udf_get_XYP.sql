CREATE FUNCTION udf_get_XYP (@slots_number tinyint, @X tinyint,@Y tinyint,@P tinyint)
RETURNS tinyint
AS
begin
DECLARE @result tinyint,
@Arg_mod1 tinyint,@Arg_mod2 tinyint,@Arg_wh1 tinyint,@Arg_wh2 tinyint,
@SmallMatrix tinyint
	if @P=0
	begin
	    set @Arg_mod1 = (@X - 1) % 3
	    set @Arg_wh1 = (@X - 1) / 3
	    set @Arg_mod2 = (@Y - 1) % 3
	    set @Arg_wh2 = (@Y - 1) / 3
	      
	    set @SmallMatrix = @Arg_mod1 - @Arg_mod2 + 1
	    If (@SmallMatrix <= 0)
		begin
			set @SmallMatrix = @SmallMatrix + 3
		end
	    set @result = 12 * @Arg_wh1 + @Arg_wh2 * 3 + @SmallMatrix
	end
	else if @Y=0
	begin
	    set @Arg_wh1 = (@P - 1) / 12
	    set @Arg_mod1 = (@X - 1) % 3
	    set @Arg_wh2 = ((@P - 1) - 12 * @Arg_wh1) / 3
	    set @Arg_mod2 = (@P - 1) % 3
	    
	    set @SmallMatrix = @Arg_mod1 - @Arg_mod2 + 1
	    If @SmallMatrix <= 0
		begin
			set @SmallMatrix = @SmallMatrix + 3
		end
	    set @result = @Arg_wh2 * 3 + @SmallMatrix
	end
	else /*@X=0*/
	begin
	    set @Arg_wh1 = (@P - 1) / 12
	    set @Arg_mod1 = (@Y - 1) % 3
	    set @Arg_mod2 = (@P - 1) % 3
	    
	    set @SmallMatrix = @Arg_mod2 + @Arg_mod1 + 1
	    If @SmallMatrix > 3 
		begin		
			set @SmallMatrix = @SmallMatrix - 3
		end
	    set @result = @Arg_wh1 * 3 + @SmallMatrix
	end
	return @result
end














