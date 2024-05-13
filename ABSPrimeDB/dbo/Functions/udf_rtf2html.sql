CREATE FUNCTION udf_rtf2html (@par varchar(8000))
RETURNS varchar(8000)
AS

BEGIN
declare @temp varchar(8000)
if charindex('\fs20',@par)!=0
begin
	set @temp=substring(@par,1,charindex('\fs20',@par))
	if (charindex('\ul',@temp)>0)
	begin
		set @temp='<u>'
	end
	else if (charindex('\i',@temp)>0)
	begin
		set @temp='<i>'
	end
	else
	begin
		set @temp=''
	end

	set @par=replace(@par,'<','&lt;')	
	set @par=replace(@par,'>','&gt;')	


	set @par=ltrim(rtrim(substring(@par,charindex('\fs20',@par)+6,8000)))	
	set @par=@temp+@par
	set @par=replace(@par,char(10)+'\par }'+char(13)+char(10),'')	
	set @par=replace(@par,char(10)+'\par','<br/>')	







	/*while charindex('\tab \tab ',@par)!=0
	begin
            		set @par=stuff(@par,charindex('<br/>',@par,charindex('\tab \tab ',@par)),5,'</span></br>')
		set @par=stuff(@par,charindex('\tab \tab ',@par),10,'<span style="margin-left: 100px;">')
	end
	while charindex('\tab ',@par)!=0
	begin
            		set @par=stuff(@par,charindex('<br/>',@par,charindex('\tab ',@par)),5,'</span></br>')
		set @par=stuff(@par,charindex('\tab ',@par),5,'<span style="margin-left: 50px;">')
	end*/





	while charindex('\up0',@par)!=0
	begin
		if (charindex('\up5',@par)<charindex('\dn5',@par) or charindex('\dn5',@par)=0) and charindex('\up5',@par)!=0
		begin
			set @par=stuff(@par,charindex('\up0',@par),4,'</sup>')
			set @par=stuff(@par,charindex('\up5',@par),4,'<sup>')
		end
		else
		begin
			set @par=stuff(@par,charindex('\up0',@par),4,'</sub>')
			set @par=stuff(@par,charindex('\dn5',@par),4,'<sub>')
		end
	end

	while charindex('\up0 ',@par)!=0
	begin
		if (charindex('\up5 ',@par)<charindex('\dn5 ',@par) or charindex('\dn5 ',@par)=0) and charindex('\up5 ',@par)!=0
		begin
			set @par=stuff(@par,charindex('\up0 ',@par),5,'</sup>')
			set @par=stuff(@par,charindex('\up5 ',@par),5,'<sup>')
		end
		else
		begin
			set @par=stuff(@par,charindex('\up0 ',@par),5,'</sub>')
			set @par=stuff(@par,charindex('\dn5 ',@par),5,'<sub>')
		end
	end

	while charindex('\dn5 ',@par)!=0
	begin
            		if charindex('\plain',@par)>0
		begin
			set @par=stuff(@par,charindex('\plain',@par,charindex('\dn5 ',@par)),6,'</sub>')
		end
		set @par=stuff(@par,charindex('\dn5 ',@par),5,'<sub>')
	end

	while charindex('\dn5',@par)!=0
	begin
            		if charindex('\plain',@par)>0
		begin
            			set @par=stuff(@par,charindex('\plain',@par,charindex('\dn5',@par)),6,'</sub>')
		end
            		set @par=stuff(@par,charindex('\dn5',@par),4,'<sub>')
	end

	while charindex('\up5 ',@par)!=0
	begin
            		if charindex('\plain',@par)>0
		begin            	
			set @par=stuff(@par,charindex('\plain',@par,charindex('\up5 ',@par)),6,'</sup>')
		end
		set @par=stuff(@par,charindex('\up5 ',@par),5,'<sup>')
	end

	while charindex('\up5',@par)!=0
	begin
            		if charindex('\plain',@par)>0
		begin
            			set @par=stuff(@par,charindex('\plain',@par,charindex('\up5',@par)),6,'</sup>')
		end
            		set @par=stuff(@par,charindex('\up5',@par),4,'<sup>')
	end

	set @par=replace(@par,'\b0 ','</b>')
	set @par=replace(@par,'\i0 ','</i>')
	set @par=replace(@par,'\ulnone ','</u>')
	set @par=replace(@par,'\b ','<b>')
	set @par=replace(@par,'\i ','<i>')
	set @par=replace(@par,'\ul ','<u>')

	set @par=replace(@par,'\b0','</b>')
	set @par=replace(@par,'\i0','</i>')
	set @par=replace(@par,'\ulnone','</u>')
	set @par=replace(@par,'\b','<b>')
	set @par=replace(@par,'\i','<i>')
	set @par=replace(@par,'\ul','<u>')


        set @par=replace(@par,'\f0 ','')
	set @par=replace(@par,'\f1 ','')
	set @par=replace(@par,'\f2 ','')
	set @par=replace(@par,'\f3 ','')
	set @par=replace(@par,'\f4 ','')
	set @par=replace(@par,'\f5 ','')
	set @par=replace(@par,'\fs20 ','')
	set @par=replace(@par,'\fs12 ','')
	set @par=replace(@par,'\fs24 ','')
	set @par=replace(@par,'\plain ','')

	set @par=replace(@par,'\f0','')
	set @par=replace(@par,'\f1','')
	set @par=replace(@par,'\f2','')
	set @par=replace(@par,'\f3','')
	set @par=replace(@par,'\f4','')
	set @par=replace(@par,'\f5','')
	set @par=replace(@par,'\fs20','')
	set @par=replace(@par,'\fs12','')
	set @par=replace(@par,'\fs24 ','')
	set @par=replace(@par,'\plain','')

        set @par=replace(@par,'\par }','')

	if (len(replace(@par,'<i>',''))<len(replace(@par,'</i>','A')))
	begin
		set @par=@par+'</i>'
	end
	if (len(replace(@par,'<b>',''))<len(replace(@par,'</b>','A')))
	begin
		set @par=@par+'</b>'
	end
	if (len(replace(@par,'<u>',''))<len(replace(@par,'</u>','A')))
	begin
		set @par=@par+'</u>'
	end
	if (len(replace(@par,'<sub>',''))<len(replace(@par,'</sub>','A')))
	begin
		set @par=@par+'</sub>'
	end
	if (len(replace(@par,'<sup>',''))<len(replace(@par,'</sup>','A')))
	begin
		set @par=@par+'</sup>'
	end

	set @par=replace(@par,'<i><b>','<b><i>')
	set @par=replace(@par,'<u><b>','<b><u>')
	set @par=replace(@par,'<u><i>','<i><u>')

	set @par=replace(@par,'</i></u>','</u></i>')
	set @par=replace(@par,'</b></u>','</u></b>')
	set @par=replace(@par,'</b></i>','</i></b>')

	set @par=replace(@par,'</b></sub>','</sub></b>')
	set @par=replace(@par,'</i></sub>','</sub></i>')
	set @par=replace(@par,'</u></sub>','</sub></u>')
	
	set @par=replace(@par,'</b></sup>','</sup></b>')
	set @par=replace(@par,'</i></sup>','</sup></i>')
	set @par=replace(@par,'</u></sup>','</sup></u>')

	if (len(replace(@par,'<u>','')) - len(replace(@par,'</u>','A'))=3)
	begin
		set @par=replace(@par,'</u>','')
	end	

end

return @par
end










