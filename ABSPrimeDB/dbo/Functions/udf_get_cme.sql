CREATE FUNCTION dbo.udf_get_cme(@candidate char(6),@app_type varchar(2))
RETURNS @T table(
	acct_num varchar(13),
	cme_req_met char(1),
	cme_to date,
	cme_from date,
	req_cme_1_adj float,
	req_cme_1_adj_sa float,
	tot_cat1 float,
	tot_cat1_sa float,
	cat1_self_non_verified float,
	cat1_self_non_verified_sa float, 				
	cat1_trans_verified float,
	cat1_trans_non_verified float,
	cat1_trans_verified_sa float,
	cat1_trans_non_verified_sa float,
	req_cme_1 float,
	req_cme_1_sa float,
	total_adj float,
	total_adj_sa float,
	abshtml varchar(max),
	abshours float,
	abshours_sa float,
	MC1T float,
	MCST float,
	training_desc_html varchar(max)
)
AS
BEGIN
	DECLARE
	@cat1_self_verified float,
	@cat1_self_non_verified float,
	@cat1_self_verified_sa float,
	@cat1_self_non_verified_sa float,
	@cat1_trans_verified float,
	@cat1_trans_non_verified float,
	@cat1_trans_verified_sa float,
	@cat1_trans_non_verified_sa float,	
	@cme_from date,
	@cme_to date,
	@tot_cat1 float,
	@tot_cat1_sa float,	

	@req_cme_1 float,
	@req_cme_1_sa float,
	@req_cme_1_adj float,
	@req_cme_1_adj_sa float,	
	@total_adj float,
	@total_adj_sa float,
	
	@training_desc varchar(max),
	@training_desc_html varchar(max),
	@training_from varchar(10),
	@training_to varchar(10),
		
	@abshtml varchar(max),
	@abshours float,
	@abshours_sa float,
	@MC1T float,
	@MCST float,

	@fTemp float,
	@sTemp varchar(max),
	@TempYear varchar(4),
		
	@YearFROM varchar(4),
	@YearTo varchar(4),
		
	@FellowshipCreditDays float,
	@CurrentYear char(4)

	SET @cat1_self_verified=0;
	SET @cat1_self_non_verified=0;
	SET @cat1_self_verified_sa=0;
	SET @cat1_self_non_verified_sa=0;
	SET @cat1_trans_verified=0;
	SET @cat1_trans_non_verified=0;
	SET @cat1_trans_verified_sa=0;
	SET @cat1_trans_non_verified_sa=0;	
	SET @tot_cat1=0;
	SET @tot_cat1_sa=0;	

	SET @req_cme_1=0;
	SET @req_cme_1_sa=0;
	SET @req_cme_1_adj=0;
	SET @req_cme_1_adj_sa=0;	
	SET @total_adj=0;
	SET @total_adj_sa=0;
	
	SET @training_desc='';
	SET @training_desc_html='';
	SET @training_from='';
	SET @training_to='';
		
	SET @abshtml='';
	SET @abshours=0;
	SET @MC1T=0;
	SET @MCST=0;

	SET @fTemp=0;
	SET @sTemp='';
	SET @TempYear='';
		
	SET @YearFROM='';
	SET @YearTo='';
		
	SET @FellowshipCreditDays=0;
	SET @CurrentYear=(SELECT rtrim(attr)+'MC' FROM mcodes WHERE grp='AY' AND code='M');

	IF CHARINDEX('CME',@app_type)>0
	BEGIN
				SELECT @cme_from=CAST(year(getdate())-4 AS varchar(4))+'-01-01',@cme_to=max(releasedate) FROM cme WHERE candidate=@candidate;

		SET @cat1_self_non_verified=ROUND((SELECT ISNULL(SUM(hours),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization='SELF' AND verified!='Yes'),1);
		SET @cat1_self_non_verified_sa=ROUND((SELECT ISNULL(SUM(credits_sa),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization='SELF' AND verified!='Yes'),1);
		SET @cat1_trans_verified=ROUND((SELECT ISNULL(SUM(hours),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified='Yes'),1);
		SET @cat1_trans_non_verified=ROUND((SELECT ISNULL(SUM(hours),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified!='Yes'),1);
		SET @cat1_trans_verified_sa=ROUND((SELECT ISNULL(SUM(credits_sa),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified='Yes'),1);
		SET @cat1_trans_non_verified_sa=ROUND((SELECT ISNULL(SUM(credits_sa),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified!='Yes'),1);
	END
	ELSE
	BEGIN
		SET @app_type=@CurrentYear+'MC';
		SELECT
			@cme_from=CAST(year(getdate())-4 AS varchar(4))+'-01-01',
			@cme_to=cast(year(getdate()) as varchar(4))+'-12-31', 
									@req_cme_1=cc_cme_req,
			@req_cme_1_sa=cc_cme_req_sa 
				FROM dbo.udf_get_cc_req(@candidate);
		SET @MC1T=(SELECT attr FROM mcodes WHERE grp='CR' AND code='MC1T');
		SET @MCST=(SELECT attr FROM mcodes WHERE grp='CR' AND code='MCST');
		SELECT 
			@abshtml=abshtml,
			@abshours=abshours,
			@abshours_sa=abshours_sa FROM dbo.udf_get_abs_cme(@candidate,@app_type,@cme_from,@cme_to);

		SET @cat1_self_non_verified=ROUND((SELECT ISNULL(SUM(hours),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization='SELF' AND verified!='Yes' AND cme.releasedate BETWEEN @cme_from AND @cme_to AND NOT (cme.reportingorganization='ABS' AND CHARINDEX('ED',cme.inv_num)=0)),1);
		SET @cat1_self_non_verified_sa=ROUND((SELECT ISNULL(SUM(credits_sa),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization='SELF' AND verified!='Yes' AND cme.releasedate BETWEEN @cme_from AND @cme_to AND NOT (cme.reportingorganization='ABS' AND CHARINDEX('ED',cme.inv_num)=0)),1);
		SET @cat1_trans_verified=ROUND((SELECT ISNULL(SUM(hours),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified='Yes' AND cme.releasedate BETWEEN @cme_from AND @cme_to AND NOT (cme.reportingorganization='ABS' AND CHARINDEX('ED',cme.inv_num)=0)),1);
		SET @cat1_trans_non_verified=ROUND((SELECT ISNULL(SUM(hours),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified!='Yes' AND cme.releasedate BETWEEN @cme_from AND @cme_to AND NOT (cme.reportingorganization='ABS' AND CHARINDEX('ED',cme.inv_num)=0)),1);	
		SET @cat1_trans_verified_sa=ROUND((SELECT ISNULL(SUM(credits_sa),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified='Yes' AND cme.releasedate BETWEEN @cme_from AND @cme_to AND NOT (cme.reportingorganization='ABS' AND CHARINDEX('ED',cme.inv_num)=0)),1);
		SET @cat1_trans_non_verified_sa=ROUND((SELECT ISNULL(SUM(credits_sa),0.0) FROM cme WHERE candidate=@candidate AND reportingorganization!='SELF' AND verified!='Yes' AND cme.releasedate BETWEEN @cme_from AND @cme_to AND NOT (cme.reportingorganization='ABS' AND CHARINDEX('ED',cme.inv_num)=0)),1);
		
		DECLARE cme_cursor CURSOR FOR 
			SELECT
				training_desc=ISNULL(q216,''),
			 	training_from=ISNULL(IIF(q19<@cme_from,@cme_from,q19),''),
				training_to=ISNULL(IIF(q20>@cme_to,@cme_to,q20),'')
			FROM ( 
		 		SELECT q216,q19,q20 FROM AppReplyCardsTraining 
		 			WHERE candidate = @candidate AND (q19 BETWEEN @cme_from AND @cme_to OR q20 BETWEEN @cme_from AND @cme_to) 
		 		) info ORDER BY training_from
		OPEN cme_cursor
		FETCH NEXT FROM cme_cursor INTO @training_desc,@training_from,@training_to

		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF LEN(@training_from)>0 AND CHARINDEX('1900',@training_from)=0
			BEGIN
				SET @YearFROM=LEFT(@training_from,4);
				SET @YearTo=LEFT(@training_to,4);

				IF @TempYear<>@YearFROM
				BEGIN
					IF LEN(@TempYear)<>0
					BEGIN
						SET @training_desc_html=@training_desc_html+
							'<tr class="blue2"><td class="tablecontent">'+@TempYear+' - '+@sTemp+'</td><td class="number">-'+FORMAT(ROUND((@fTemp/30.4*2.5*10)/10,1),'N')+'</td><td class="number">-'+FORMAT(ROUND((@fTemp/30.4*1.7*10)/10,1),'N')+'</td></tr>';	
					END								
						
					SET @fTemp=0;
					SET @TempYear=@YearFROM;
					SET @sTemp='';
				END				
				
				SET @sTemp=@sTemp+IIF(LEN(ISNULL(@sTemp,''))<>0,', ','')+@training_desc;
				IF @YearFROM=@YearTo
				BEGIN
					SET @fTemp=@fTemp+DATEDIFF(day,CAST(@training_from as date),CAST(@training_to as date));
				END
				ELSE
				BEGIN
					SET @fTemp=@fTemp+DATEDIFF(day,CAST(@training_from as date),CAST(CAST(@YearFROM+1 as varchar(4))+'-01-01' as date));
					SET @TempYear=@YearTo;
					
					SET @FellowshipCreditDays=@FellowshipCreditDays+@fTemp;
					IF @TempYear<>@YearFROM
					BEGIN
						SET @training_desc_html=@training_desc_html+
							'<tr class="blue2"><td class="tablecontent">'+@YearFROM+' - '+@sTemp+'</td><td class="number">-'+FORMAT(ROUND((@fTemp/30.4*2.5*10)/10,1),'N')+'</td><td class="number">-'+FORMAT(ROUND((@fTemp/30.4*1.7*10)/10,1),'N')+'</td></tr>';				
					END							
					SET @fTemp=DATEDIFF(day,CAST(CAST(@YearTo as varchar(4))+'-01-01' as date),CAST(@training_to as date));	
				END
				SET @FellowshipCreditDays=@FellowshipCreditDays+@fTemp;						
			END

			FETCH NEXT FROM cme_cursor INTO @training_desc,@training_from,@training_to
		END
		
		CLOSE cme_cursor
		DEALLOCATE cme_cursor
	END	

	SET @total_adj=ROUND(@abshours+@FellowshipCreditDays/30.4*2.5,1);
	SET @total_adj_sa=ROUND(@abshours_sa+@FellowshipCreditDays/30.4*1.7,1);


	SET @total_adj=IIF(@req_cme_1-@total_adj>0,@total_adj,@req_cme_1);
	SET @total_adj_sa=IIF(@req_cme_1_sa-@total_adj_sa>0,@total_adj_sa,@req_cme_1_sa);
	
	
	SET @req_cme_1_adj=@req_cme_1-@total_adj;
	SET @req_cme_1_adj_sa=@req_cme_1_sa-@total_adj_sa;
	
	SET @tot_cat1=@cat1_self_non_verified+@cat1_trans_verified+@cat1_trans_non_verified;
	SET @tot_cat1_sa=@cat1_self_non_verified_sa+@cat1_trans_verified_sa+@cat1_trans_non_verified_sa;	

	INSERT INTO @T
		(
			acct_num,
			cme_req_met,
			cme_to,
			cme_from,
			req_cme_1_adj,
			req_cme_1_adj_sa,
			tot_cat1,
			tot_cat1_sa,
			cat1_self_non_verified,
			cat1_self_non_verified_sa, 				
			cat1_trans_verified,
			cat1_trans_non_verified,
			cat1_trans_verified_sa,
			cat1_trans_non_verified_sa,
			req_cme_1,
			req_cme_1_sa,
			total_adj,
			total_adj_sa,
			abshtml ,
			abshours ,
			MC1T,
			MCST,
			training_desc_html			
		)
	SELECT 
		acct_num=(SELECT acct_num FROM surgeon WHERE candidate=@candidate),
		
		cme_req_met=IIF(@tot_cat1>=@req_cme_1_adj AND @tot_cat1_sa>=@req_cme_1_adj_sa,'Y','N'),			

		cme_to=ISNULL(convert(varchar,@cme_to,21),''),
		cme_from=ISNULL(convert(varchar,@cme_from,21),''),

		req_cme_1_adj=@req_cme_1_adj,
		req_cme_1_adj_sa=@req_cme_1_adj_sa,

		tot_cat1=@tot_cat1,
		tot_cat1_sa=@tot_cat1_sa,
						
		cat1_self_non_verified=@cat1_self_non_verified,
		cat1_self_non_verified_sa=@cat1_self_non_verified_sa, 				
		cat1_trans_verified=@cat1_trans_verified,
		cat1_trans_non_verified=@cat1_trans_non_verified,
		cat1_trans_verified_sa=@cat1_trans_verified_sa,
		cat1_trans_non_verified_sa=@cat1_trans_non_verified_sa,
		req_cme_1=@req_cme_1,
		req_cme_1_sa=@req_cme_1_sa,

		total_adj=@total_adj,
		total_adj_sa=@total_adj_sa,
			
		abshtml=@abshtml ,
		abshours=@abshours ,
		MC1T=@MC1T,
		MCST=@MCST,
		
		training_desc_html=@training_desc_html+IIF(@YearFROM<>'0','<tr class="blue2"><td class="tablecontent">'+cast(@YearFROM as varchar(4))+' - '+@sTemp+'</td><td class="number">-'+FORMAT(ROUND((@fTemp/30.4*2.5*10)/10,1),'N')+'</td><td class="number">-'+FORMAT(ROUND((@fTemp/30.4*1.7*10)/10,1),'N')+'</td></tr>','');			
  
		RETURN
END