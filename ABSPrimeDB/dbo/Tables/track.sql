CREATE TABLE [dbo].[track] (
    [created]            [dbo].[created]       NOT NULL,
    [creator]            [dbo].[creator]       NOT NULL,
    [modified]           [dbo].[modified]      NULL,
    [modifier]           [dbo].[modifier]      NULL,
    [modifications]      [dbo].[modifications] NOT NULL,
    [trackno]            CHAR (6)              NOT NULL,
    [UserId]             INT                   NULL,
    [candidate]          CHAR (6)              NULL,
    [exam]               [dbo].[exam]          NOT NULL,
    [type]               CHAR (1)              NOT NULL,
    [year]               SMALLINT              NOT NULL,
    [pre_send]           SMALLDATETIME         NULL,
    [pre_receive]        SMALLDATETIME         NULL,
    [pre_approve]        SMALLDATETIME         NULL,
    [pre_disapprove]     SMALLDATETIME         NULL,
    [pre_defer]          SMALLDATETIME         NULL,
    [pre_credential]     SMALLDATETIME         NULL,
    [app_send]           SMALLDATETIME         NULL,
    [app_receive]        SMALLDATETIME         NULL,
    [app_approve]        SMALLDATETIME         NULL,
    [app_disapprove]     SMALLDATETIME         NULL,
    [app_defer]          SMALLDATETIME         NULL,
    [app_credential]     SMALLDATETIME         NULL,
    [oper_receive]       SMALLDATETIME         NULL,
    [oper_approve]       SMALLDATETIME         NULL,
    [card_receive]       SMALLDATETIME         NULL,
    [lic_receive]        SMALLDATETIME         NULL,
    [reg_receive]        SMALLDATETIME         NULL,
    [let_send]           SMALLDATETIME         NULL,
    [let_receive]        SMALLDATETIME         NULL,
    [fee_amount]         MONEY                 NULL,
    [fee_receive]        SMALLDATETIME         NULL,
    [fee_bounce]         SMALLDATETIME         NULL,
    [program]            CHAR (4)              NULL,
    [_group]             CHAR (1)              NULL,
    [compyear]           SMALLINT              NULL,
    [note]               VARCHAR (8000)        NULL,
    [id]                 NUMERIC (9)           IDENTITY (1, 1) NOT NULL,
    [trans_id]           VARCHAR (36)          NULL,
    [trans_log]          TEXT                  NULL,
    [sig_receive]        SMALLDATETIME         NULL,
    [certnotice_receive] SMALLDATETIME         NULL,
    [cme_receive]        SMALLDATETIME         NULL,
    [web_note]           VARCHAR (8000)        NULL,
    [cos_receive]        SMALLDATETIME         NULL,
    [coc_receive]        SMALLDATETIME         NULL,
    [other_receive]      SMALLDATETIME         NULL,
    [cos_from]           VARCHAR (50)          NULL,
    [coc_from]           VARCHAR (50)          NULL,
    [other_from]         VARCHAR (50)          NULL,
    [app_complete]       SMALLDATETIME         NULL,
    [other2_receive]     SMALLDATETIME         NULL,
    [other2_form]        VARCHAR (50)          NULL,
    [paid_in_full]       BIT                   CONSTRAINT [DF_track_paid_in_full] DEFAULT ((0)) NOT NULL,
    [v_fellowship]       TINYINT               NULL,
    [surv_receive]       SMALLDATETIME         NULL,
    [examcode]           AS                    ((CONVERT([varchar],[year],(0))+[exam])+[type]),
    [acls_receive]       SMALLDATETIME         NULL,
    [atls_receive]       SMALLDATETIME         NULL,
    [fls_receive]        SMALLDATETIME         NULL,
    [fes_receive]        SMALLDATETIME         NULL,
    [rpvi_receive]       SMALLDATETIME         NULL,
    [req_cme_1]          INT                   NULL,
    [req_cme_1_sa]       INT                   NULL,
    [req_cme_tot]        INT                   NULL,
    [cme_from]           SMALLDATETIME         NULL,
    [cme_to]             SMALLDATETIME         NULL,
    [packet_receive]     SMALLDATETIME         NULL,
    [pd_approve]         SMALLDATETIME         NULL,
    [fpd_receive]        SMALLDATETIME         NULL,
    [fpd_approve]        SMALLDATETIME         NULL,
    [app_exception]      VARCHAR (1)           NULL,
    CONSTRAINT [PK_track] PRIMARY KEY CLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
	CONSTRAINT [FK_track_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [track_cn]
    ON [dbo].[track]([candidate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE UNIQUE NONCLUSTERED INDEX [track_uc]
    ON [dbo].[track]([trackno] ASC, [exam] ASC, [type] ASC, [year] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX ['nc_examcode']
    ON [dbo].[track]([examcode] ASC)
    INCLUDE([id]) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER [dbo].[tr_track_update] ON [dbo].[track]
FOR  UPDATE ,INSERT
AS

DECLARE @ID numeric,@ISUPDATE int,@exam char(1),@type char(1),@candidate char(6), @UserId int, @examcode varchar(8)

select @ISUPDATE=count(*) from deleted

select @ID=id,@exam=exam,@type=type,@candidate=candidate,@UserId=UserId,@examcode=examcode from inserted

if @type='R' AND not exists (select exam.id from exam,inserted where exam.candidate=inserted.candidate and exam.trackcode=inserted.examcode and inserted.id=@ID)
begin
	insert into exam (candidate,year,exam,type,creator,created,status,area) select candidate,year,exam,dbo.udf_type_te(exam,type),'trk',getdate(),'T',IIF(exam in ('G','V','C','P','O','H'),'0','') from inserted where id=@ID
end	

if 	(
		((select app_approve from inserted where id=@ID) IS NOT NULL AND ((select app_approve from deleted where id=@ID) IS NULL OR @ISUPDATE=0)) 
			OR 
		((select sig_receive from inserted where id=@ID) IS NOT NULL AND ((select sig_receive from deleted where id=@ID) IS NULL OR @ISUPDATE=0) AND @type IN ('Q','C'))
	) AND 
	@type not in ('X','R','N') AND 
	@exam!='M'
begin
	-- if not exists (select exam.id from exam,inserted where exam.candidate=inserted.candidate and exam.trackcode=inserted.examcode and inserted.id=@ID)
	-- begin
	-- 	-- insert into exam (candidate,year,exam,type,creator,created,status,area) select candidate,year,exam,dbo.udf_type_te(exam,type),'trk',getdate(),'T','' from inserted where id=@ID
	-- end
	-- else 
	if @type='R'
	begin
		UPDATE exam SET status='R' WHERE candidate=@candidate and examcode=@examcode;
	end

	if @type='B' 
	begin
		UPDATE track SET app_receive=getdate() WHERE id=@id;
		UPDATE exam SET status='R' WHERE candidate=@candidate and examcode=@examcode;
	end
	else if @type!='P' and ((select app_approve from inserted where id=@ID) IS NOT NULL AND ((select app_approve from deleted where id=@ID) IS NULL OR @ISUPDATE=0)) 
	begin
		DECLARE @year_start as int, @year_end as int, @admissible as char(1)
		
		set @admissible='Y'
		if @exam not in ('G','V') 
		begin
			if not exists (select id from exam_pass where exam in ('G','V') and isactive=1 and candidate=@candidate)
			begin 
				set @admissible='I'
			end
		end
		
		select @year_start=a.compyear,@year_end=a.compyear+4 from inserted 
			inner join (select max(compyear) compyear,candidate,exam from candidate_program group by candidate,exam) a on a.candidate=inserted.candidate and a.exam=inserted.exam
			where id=@ID and 
			((inserted.exam in ('G','V') and a.compyear>2012) or (inserted.exam='O' and a.compyear>2018)) and 
			@type!='R' 
	
		if exists (select e.candidate from exam_eligibility e,inserted where  
			e.candidate=inserted.candidate and e.exam=inserted.exam and e.type=dbo.udf_type_te(inserted.exam,inserted.type) and inserted.id=@ID)
		begin
			update exam_eligibility
				set candidate=inserted.candidate,exam=inserted.exam,type=dbo.udf_type_te(inserted.exam,inserted.type),
				admissible=@admissible,year_start=ISNULL(@year_start,year(ISNULL(inserted.app_approve,inserted.sig_receive))),year_end=ISNULL(@year_end,year(ISNULL(inserted.app_approve,inserted.sig_receive))+5),
				modifier='trk',modified=getdate(),modifications=e.modifications+1
			from exam_eligibility e
			inner join inserted ON e.candidate=inserted.candidate and e.exam=inserted.exam and e.type=inserted.type
			where e.admissible='N'
		end
		else
		begin
			insert into exam_eligibility
	 			(candidate, exam, type, year_start, year_end, examstaken, admissible,creator) 
	  		select candidate,exam,dbo.udf_type_te(exam,type),ISNULL(@year_start,year(ISNULL(inserted.app_approve,inserted.sig_receive))),ISNULL(@year_end,year(ISNULL(inserted.app_approve,inserted.sig_receive))+5),0,@admissible,'trk'
				from inserted
	
		end
	end
end

if @ISUPDATE=0
begin
	/*if @exam='M'
	begin
		DECLARE @cc_status int,@inv_num varchar(16),@inv_num_pastyear varchar(16),@year int,@moc_ay smallint
		SELECT @cc_status=cc_status FROM dbo.udf_get_cc_req(@candidate);
		SELECT 
			@inv_num=z.candidate+cast(z.year AS varchar)+'MCA',
			@inv_num_pastyear=z.candidate+cast(z.year-1 AS varchar)+'MCA',
			@year=year,
			@moc_ay=dbo.udf_get_academic_year('M')
		FROM inserted z;
		
		/
			7 --continuous/not expired
			6 --time-limited expired
			4 --time-limited lapsed
			5 --lifetime
			3 --time-limited non-compliant
			1 --time-limited compliant
		/		
		
		IF @cc_status IN (3,4,6,7)
		BEGIN
			INSERT INTO invoice (creator,inv_num,type,ref_id) 
				VALUES ('trk',@inv_num,'A',@ID);

			IF @cc_status = 7
			BEGIN
				INSERT INTO inv_det (creator,inv_num,code,amount,quantity) 
					SELECT 'trk',@inv_num,code,attr,1 FROM mcodes 
					INNER JOIN moc_eligibility ON moc_eligibility.candidate=@candidate AND LEFT(cmoc_examcode,6) NOT IN (CAST(@moc_ay-1 AS char(4))+'GO',CAST(@moc_ay-2 AS char(4))+'GO') AND cmoc_cohort<CAST(@moc_ay AS char(4))
					WHERE grp='FC' AND code='CCAF'+CAST(@moc_ay AS char(4)) AND attr2='1'
						
			END
			
			IF @cc_status IN (3,4,6)
			BEGIN
				INSERT INTO inv_det (creator,inv_num,code,amount,quantity) 
					SELECT 'trk',@inv_num,code,attr,1 FROM mcodes 
					WHERE grp='FC' AND attr2='1' AND code=
						CASE 
							WHEN @cc_status = 3 THEN 'CCAL' 
							WHEN @cc_status IN (4,6) THEN 'LPSD'
						END
			END
		END
	END
	ELSE*/
	IF @type!='R' AND @exam!='M'
	begin
		if not exists (select inv_num from invoice where inv_num=cast(@UserId as varchar)+@examcode+'A')
		begin
			insert into invoice (creator,UserId,inv_num,type,ref_id) 
				select 'trk',
					z.UserId,
					cast(z.UserId as varchar)+cast(z.year as varchar)+z.exam+dbo.udf_type_te(z.exam,z.type)+'A',
					'A',id 
				from inserted z
				where id=@ID
		end
		if not exists (select inv_num from inv_det where inv_num=cast(@UserId as varchar)+@examcode+'A')
		begin
		insert into inv_det (creator,UserId,inv_num,code,amount,quantity) 
			select 'trk',
				z.UserId,
				cast(z.UserId as varchar)+cast(z.year as varchar)+z.exam+dbo.udf_type_te(z.exam,z.type)+'A',
				case isnull(a.code,'') when '' then z.exam+dbo.udf_type_te(z.exam,z.type)+'AT' else a.code end,
				cast(isnull(a.attr,isnull(b.attr,0)) as money),1 
			from inserted z
				left outer join mcodes a on left(a.code,3)=z.exam+dbo.udf_type_te(z.exam,z.type)+'A' and a.attr2='1' and a.grp='FC'
				left outer join mcodes b on b.code=z.exam+dbo.udf_type_te(z.exam,z.type)+'AT' and b.attr2='1' and b.grp='FC'
			where z.id=@ID
		end
	end	

	update track set req_cme_1=cme_1.attr,req_cme_1_sa=cme_1_sa.attr,req_cme_tot=cme_tot.attr
	from track
		inner join mcodes cme_1 on @exam+@type+'1T'=cme_1.code and cme_1.grp='CR'
		inner join mcodes cme_1_sa on @exam+@type+'ST'=cme_1_sa.code and cme_1_sa.grp='CR'
		inner join mcodes cme_tot on @exam+@type+'TT'=cme_tot.code and cme_tot.grp='CR'
	where
		track.id=@ID

		
end

IF @exam!='M' and @type!='N' 
BEGIN
	IF 	ISNULL((select pd_approve from inserted),getdate())!=ISNULL((select pd_approve from deleted),getdate()) AND
		ISNULL((SELECT let_type FROM AppRefLet WHERE exam_type=@examcode AND candidate=@candidate),0)=10
	BEGIN
		UPDATE AppRefLet SET let_approve=(select pd_approve from inserted)
		WHERE 
			candidate=@candidate AND 
			exam_type=@examcode AND 
			ISNULL(let_approve,getdate())!=ISNULL((select pd_approve from inserted),getdate())
	END
END