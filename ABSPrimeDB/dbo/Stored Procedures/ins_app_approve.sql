CREATE proc [dbo].[ins_app_approve]
    @candidate as char(6),
    @examcode as varchar(8)
AS
    DECLARE
        @trackid numeric,
        @app_receive smalldatetime,
        @app_approve smalldatetime,
        @isanyopexvalid tinyint,
        @fee_balance money,
        @sig_receive smalldatetime, 
        @LicenseExpireDate smalldatetime,
        @status_note_len int,
        @admissible char(1),
        @aal char(6),
        @ad varchar(10),
        @cme_req_met char(1),
        @cc_moc_req_met char(1),
        @cc_status int,
        @cc_refletinstancesreq int,
        @cc_refletreceivedinstances int,
        @cc_refletsentinstances int,
        @cc_refletexpired int,
        @cc_refletadverse varchar(100),
        @cc_refletnotapproved varchar(100),
        @num_cycles_non_compliant tinyint,
        @note varchar(1000),
        @lapsed_year tinyint
        
    SET NOCOUNT ON
    BEGIN
        SELECT @cme_req_met=cme_req_met FROM dbo.udf_get_cme(@candidate,'MC');
        
        select
            @trackid=trackid,
            @app_receive=app_receive,
            @app_approve=app_approve,
            @isanyopexvalid=isanyopexvalid,
            @fee_balance=fee_balance,
            @sig_receive=sig_receive, 
            @LicenseExpireDate=LicenseExpireDate,
            @status_note_len=status_note_len,
            @admissible=admissible,
            @aal=aal,
            @ad=ad,
            @cc_moc_req_met=cc_moc_req_met,
            @cc_status=cc_status,
            @cc_refletinstancesreq=cc_refletinstancesreq,
            @cc_refletreceivedinstances=cc_refletreceivedinstances,
            @cc_refletsentinstances=cc_refletsentinstances,
            @cc_refletadverse=cc_refletadverse,
            @cc_refletnotapproved=cc_refletnotapproved,
            @cc_refletexpired=cc_refletexpired,
            @num_cycles_non_compliant=num_cycles_non_compliant
        from dbo.udf_get_cc_status(@candidate,@examcode);
        
        SELECT @lapsed_year=ISNULL(dbo.udf_get_academic_year('M')-YEAR(surgeon_st.start_date)+1,0) FROM surgeon_st WHERE candidate=@candidate AND status_code='LP' AND @cc_status=7;
        SET @lapsed_year=ISNULL(@lapsed_year,0);
      
        BEGIN
            IF (@app_approve IS NULL)
            BEGIN
                IF (   
                    @app_receive IS NOT NULL AND
                    @sig_receive IS NOT NULL AND
                    @fee_balance<=0 AND
                    @aal IS NULL AND
                    (@ad IS NULL OR cast(@ad as date)>=cast(getdate() as date)) 
                    AND	
                    (
                    	(
                    		CHARINDEX('R',@examcode)>0 AND
                    		(SELECT app_approve FROM track WHERE candidate=@candidate AND examcode=LEFT(@examcode,4)+'MC') IS NOT NULL
                    	)
	                    OR
	                    (
	                   		CHARINDEX('B',@examcode)>0 
	                    )
	                    OR
	                    (
	                   		CHARINDEX('N',@examcode)>0 
	                    )	                    
	                    OR
	                    (
	                        (@isanyopexvalid>0 OR CHARINDEX('R',@examcode)>0 OR CHARINDEX('MC',@examcode)>0) AND
		                    @LicenseExpireDate IS NOT NULL AND
		                    @status_note_len = 0 AND
		                    (   		                    	(rtrim(@cc_refletnotapproved)='' AND @cc_refletexpired=0 AND @cc_refletreceivedinstances>=@cc_refletinstancesreq)
		                    	OR
		                    	(@lapsed_year=0 AND @cc_refletexpired=0  AND @cc_refletsentinstances>=@cc_refletinstancesreq) 
		                    )
		                    AND @cme_req_met='Y'
		               	)
	               	)
                 )     
                BEGIN
                    UPDATE track SET app_approve=getdate(),pre_approve=getdate() WHERE id=@trackid;                  
                END
                ELSE 
                BEGIN
                    IF EXISTS (SELECT id FROM surgeon_st WHERE candidate=@candidate AND status_code='CA'+@examcode)
                    BEGIN
                        SELECT @note=ISNULL(note,'') FROM surgeon_st WHERE candidate=@candidate AND status_code='CA'+@examcode; 
                    END

					IF @isanyopexvalid=0 AND CHARINDEX('R',@examcode)=0 AND CHARINDEX('MC',@examcode)=0 
					BEGIN
					    IF EXISTS (SELECT id FROM surgeon_st WHERE candidate=@candidate AND status_code='CA'+@examcode)
					    BEGIN
					        UPDATE surgeon_st SET modified=getdate(), modifications=modifications+1 ,modifier='iaa', note=isnull(note,'')+'1,'  
					            WHERE candidate=@candidate AND status_code='CA'+@examcode AND isnull(note,'') not like '1,%' AND isnull(note,'') not like '%,1,%' 
					    END
					 ELSE
					    BEGIN 
					        INSERT INTO surgeon_st (creator,created,modifications,candidate,status_code,note) VALUES ('iaa',getdate(),0,@candidate,'CA'+@examcode,'1,') 
					    END                     
					END
					ELSE
					BEGIN
					    UPDATE surgeon_st SET modified=getdate(), modifications=modifications+1 ,modifier='iaa', note=REPLACE(isnull(note,''),',1,',',')  
					        WHERE candidate=@candidate AND status_code='CA'+@examcode AND isnull(note,'') like '%,1,%';
					    UPDATE surgeon_st SET modified=getdate(), modifications=modifications+1 ,modifier='iaa', note=REPLACE(isnull(note,''),'1,','')  
					        WHERE candidate=@candidate AND status_code='CA'+@examcode AND isnull(note,'') like '1,%';
					END
				
					IF (@LicenseExpireDate IS NULL) 
                    BEGIN
                    	IF EXISTS (SELECT id FROM surgeon_st WHERE candidate=@candidate AND status_code='CA'+@examcode)
                    	BEGIN
                    		UPDATE surgeon_st SET modified=getdate(), modifications=modifications+1 ,modifier='iaa', note=isnull(note,'')+'39,'  
                    			WHERE candidate=@candidate AND status_code='CA'+@examcode AND isnull(note,'') not like '%39,%' 
                    	END
                   		ELSE
                    	BEGIN 
                    		INSERT INTO surgeon_st (creator,created,modifications,candidate,status_code,note) VALUES ('iaa',getdate(),0,@candidate,'CA'+@examcode,'39,') 
                    	END                     
                    END
    				ELSE
                    BEGIN
                    	UPDATE surgeon_st SET modified=getdate(), modifications=modifications+1 ,modifier='iaa', note=REPLACE(isnull(note,''),'39,','')  
                    		WHERE candidate=@candidate AND status_code='CA'+@examcode AND isnull(note,'') like '%39,%' 
             		END                       
                END
            END
        END
    END