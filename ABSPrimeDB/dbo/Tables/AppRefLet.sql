CREATE TABLE [dbo].[AppRefLet] (
    [id]                      NUMERIC (18)          IDENTITY (1, 1) NOT NULL,
    [created]                 [dbo].[created]       NULL,
    [creator]                 [dbo].[creator]       NULL,
    [modified]                [dbo].[modified]      NULL,
    [modifier]                [dbo].[modifier]      NULL,
    [modifications]           [dbo].[modifications] NOT NULL,
	[UserId]                  INT              		NULL,
    [candidate]               CHAR (6)              NULL,
    [exam_type]               VARCHAR (8)           NOT NULL,
    [let_type]                TINYINT               NOT NULL,
    [alternaterole]           VARCHAR (200)         NULL,
    [explain]                 VARCHAR (8000)        NULL,
    [official]                VARCHAR (85)          NULL,
    [coordinator]             VARCHAR (85)          NULL,
    [title]                   VARCHAR (50)          NULL,
    [address]                 VARCHAR (200)         NULL,
    [city]                    VARCHAR (30)          NULL,
    [state]                   VARCHAR (2)           NULL,
    [zip]                     VARCHAR (10)          NULL,
    [email]                   VARCHAR (100)         NULL,
    [coordinator_email]       VARCHAR (100)         NULL,
    [phone]                   VARCHAR (100)         NULL,
    [hosp]                    VARCHAR (356)         NULL,
    [let_rcvd]                DATETIME              NULL,
    [coordinator_rcvd]        DATETIME              NULL,
    [response_ip]             VARCHAR (15)          NULL,
    [let_sent]                DATETIME              NULL,
    [let_approve]             DATETIME              NULL,
    [sameinstitution]         VARCHAR (5)           NULL,
    [fullprivileges]          VARCHAR (5)           NULL,
    [professionalinteraction] VARCHAR (5)           NULL,
    [personalrelationship]    VARCHAR (5)           NULL,
    [acceptableskills]        VARCHAR (5)           NULL,
    [dependable]              VARCHAR (5)           NULL,
    [communicationskills]     VARCHAR (5)           NULL,
    [behavior]                VARCHAR (5)           NULL,
    [participatesactivities]  VARCHAR (5)           NULL,
    [substanceabuse]          VARCHAR (5)           NULL,
    [disciplinaryactions]     VARCHAR (5)           NULL,
    [recommend]               VARCHAR (5)           NULL,
    [pd_completedres]         VARCHAR (5)           NULL,
    [pd_sixoperative]         VARCHAR (5)           NULL,
    [pd_sixclinical]          VARCHAR (5)           NULL,
    [pd_endoscopy]            VARCHAR (5)           NULL,
    [scope]                   VARCHAR (5)           NULL,
    [remarks]                 VARCHAR (8000)        NULL,
    [signature]               VARCHAR (5)           NULL,
    [coordinator_signature]   VARCHAR (5)           NULL,
    [pd_confirm]              VARCHAR (5)           NULL,
    [coordinator_confirm]     VARCHAR (5)           NULL,
    [sec_order]               TINYINT               NULL,
    [id_code]                 VARCHAR (100)         NULL,
    [adversecount]            AS                    (case [sec_order] when '10' then case when [exam_type] like '%GQ' then ((((case when isnull([dependable],'')='Yes' then (0) else (1) end+case when isnull([pd_completedres],'')='Yes' then (0) else (1) end)+case when isnull([pd_sixoperative],'')='Yes' then (0) else (1) end)+case when isnull([pd_sixclinical],'')='Yes' then (0) else (1) end)+case when isnull([pd_endoscopy],'')='Yes' then (0) else (1) end)+case when isnull([recommend],'')='Yes' then (0) else (1) end when [exam_type] like '%B' then case when isnull([pd_confirm],'')='Yes' OR len(rtrim(isnull([official],'')))=(0) then (0) else (1) end+case when isnull([coordinator_confirm],'')='Yes' then (0) else (1) end else (case when isnull([dependable],'')='Yes' then (0) else (1) end+case when isnull([pd_completedres],'')='Yes' then (0) else (1) end)+case when isnull([recommend],'')='Yes' then (0) else (1) end end else (((((((((((case when isnull([sameinstitution],'')='Yes' then (0) else (1) end+case when isnull([fullprivileges],'')='Yes' then (0) else (1) end)+case when isnull([professionalinteraction],'')='Yes' then (0) else (1) end)+case when isnull([personalrelationship],'')='Yes' then (0) else (1) end)+case when isnull([acceptableskills],'')='Yes' then (0) else (1) end)+case when isnull([dependable],'')='Yes' then (0) else (1) end)+case when isnull([communicationskills],'')='Yes' then (0) else (1) end)+case when isnull([behavior],'')='Yes' then (0) else (1) end)+case when isnull([participatesactivities],'')='Yes' then (0) else (1) end)+case when isnull([substanceabuse],'')='Yes' then (0) else (1) end)+case when isnull([disciplinaryactions],'')='Yes' then (0) else (1) end)+case when isnull([recommend],'')='Yes' then (0) else (1) end)+case when isnull([scope],'')='Yes' then (0) else (1) end end),
	[pd_chiefweeks] varchar (5) NULL,
	[pd_pg4chieflet] varchar (5) NULL,
	[pd_reqhours] varchar (5) NULL,
	[pd_extension] varchar (5) NULL,
	[pd_flexrots] varchar (5) NULL,
	[pd_introts] varchar (5) NULL,
	[pd_research] varchar (5) NULL,
	[pd_reqweeks] varchar (5) NULL,
    [pd_documents]            VARCHAR (5)           NULL,
	CONSTRAINT [FK_appreflet_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [IX_AppRefLet]
    ON [dbo].[AppRefLet]([candidate] ASC, [exam_type] ASC, [let_type] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_14019_14018_AppRefLet]
    ON [dbo].[AppRefLet]([id] ASC)
    INCLUDE([modifications], [candidate], [exam_type], [let_type]);


GO
CREATE NONCLUSTERED INDEX [IX_14070_14069_AppRefLet]
    ON [dbo].[AppRefLet]([exam_type] ASC, [let_approve] ASC, [signature] ASC, [let_rcvd] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_14072_14071_AppRefLet]
    ON [dbo].[AppRefLet]([id_code] ASC)
    INCLUDE([id], [candidate], [let_type], [alternaterole], [explain], [pd_endoscopy], [scope], [remarks], [signature], [sec_order], [substanceabuse], [disciplinaryactions], [recommend], [pd_completedres], [pd_sixoperative], [pd_sixclinical], [personalrelationship], [acceptableskills], [dependable], [communicationskills], [behavior], [participatesactivities], [email], [phone], [hosp], [sameinstitution], [fullprivileges], [professionalinteraction], [official], [title], [address], [city], [state], [zip]);


GO
CREATE NONCLUSTERED INDEX [IX_14074_14073_AppRefLet]
    ON [dbo].[AppRefLet]([id_code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_34229_34228_AppRefLet]
    ON [dbo].[AppRefLet]([let_type] ASC)
    INCLUDE([candidate], [exam_type]);


GO
CREATE NONCLUSTERED INDEX [IX_34259_34258_AppRefLet]
    ON [dbo].[AppRefLet]([let_type] ASC)
    INCLUDE([candidate], [exam_type], [official]);


GO
CREATE TRIGGER [dbo].[tr_appreflet_update] ON [dbo].[AppRefLet]
FOR  UPDATE, INSERT, DELETE
AS

DECLARE @candidate char(6), @examcode varchar(8), @isjustapproved bit, @let_type tinyint, @coordinator_confirm varchar(5), @coordinator_signature varchar(5)
if (select count(*) from inserted) != 0
BEGIN
	select @candidate=candidate,@examcode=exam_type,@let_type=let_type,@coordinator_confirm=coordinator_confirm,@coordinator_signature=coordinator_signature from inserted
	set @isjustapproved=0
	
	IF 	
												(
			(select let_rcvd from inserted where isnull(official,'')!='' and isnull(signature,'')='true') IS NOT NULL 
				OR
			(select coordinator_rcvd from inserted where isnull(coordinator,'')!='' and isnull(coordinator_signature,'')='true' and isnull(official,'')='') IS NOT NULL
		)
			AND
		((select isnull(recommend,'') from inserted)='Yes' OR CHARINDEX('B',@examcode)>0 OR CHARINDEX('N',@examcode)>0)
			AND
		(
						(@let_type!=10 AND
				(select isnull(substanceabuse,'') from inserted)='Yes'
					AND
				(select isnull(disciplinaryactions,'') from inserted)='Yes'
			)
				OR 
						(@let_type=10 AND @examcode NOT LIKE '%GQ' AND
				(select isnull(dependable,'') from inserted)='Yes'
					AND
				(select isnull(pd_completedres,'') from inserted)='Yes'
			)
				OR
						(@let_type=10 AND @examcode LIKE '%GQ' AND
				(select isnull(dependable,'') from inserted)='Yes'
					AND
				(select isnull(pd_completedres,'') from inserted)='Yes'
					AND
				(select isnull(pd_sixoperative,'') from inserted)='Yes'
					AND
				(select isnull(pd_sixclinical,'') from inserted)='Yes'
					AND
				(select isnull(pd_endoscopy,'') from inserted)='Yes'
			)
				OR
						(@let_type=10 AND @examcode LIKE '%B' AND
				(select IIF(LEN(RTRIM(ISNULL(official,'')))=0,'Yes','')+isnull(pd_confirm,'') from inserted)='Yes'
					AND
				(select isnull(coordinator_confirm,'') from inserted)='Yes'
			)
				OR
						(@examcode LIKE '%CN' AND
				(select isnull(pd_confirm,'') from inserted)='Yes'
			)	
		)
			AND
							(
			(select let_approve from inserted) IS NULL  
				OR
			(select isnull(let_rcvd, '') from inserted)!=(select isnull(let_rcvd, '') from deleted)
				OR
			(select isnull(coordinator_rcvd, '') from inserted)!=(select isnull(coordinator_rcvd, '') from deleted)
		)		
	BEGIN
	    UPDATE appreflet SET let_approve = GETDATE(), modifier='tau', modifications=appreflet.modifications+1
	    FROM appreflet
	    JOIN inserted ON appreflet.ID = inserted.ID

	    set @isjustapproved=1
	END
	
	IF 	@let_type=10 AND 
		(@isjustapproved=1 OR ISNULL((select let_approve from inserted),getdate())!=ISNULL((select let_approve from deleted),getdate()))
    BEGIN
    	UPDATE track SET pd_approve=IIF(@isjustapproved=1,getdate(),(select let_approve from inserted)) 
    	WHERE 
    		candidate=@candidate AND 
    		examcode=@examcode AND 
    		ISNULL(IIF(@isjustapproved=1,getdate(),(select let_approve from inserted)),getdate())!=ISNULL((SELECT pd_approve FROM track WHERE candidate=@candidate AND examcode=@examcode),getdate())
    		
    	IF @coordinator_confirm='Yes' AND @coordinator_signature='true'
    	BEGIN
	    	UPDATE track SET fpd_approve=IIF(@isjustapproved=1,getdate(),(select let_approve from inserted)) 
	    	WHERE 
	    		candidate=@candidate AND 
	    		examcode=@examcode AND 
	    		ISNULL(IIF(@isjustapproved=1,getdate(),(select let_approve from inserted)),getdate())!=ISNULL((SELECT fpd_approve FROM track WHERE candidate=@candidate AND examcode=@examcode),getdate())
    	
    	END
    END
	
	IF	(
			((select let_rcvd from inserted) IS NOT NULL AND (select let_rcvd from deleted) IS NULL)
				OR 
			@isjustapproved=1
				OR
			((select let_approve from inserted) IS NOT NULL AND (select let_approve from deleted) IS NULL) 					
		) 
			AND 
		(select count(*) from deleted)!=0 
			AND 
		CHARINDEX('B',@examcode)=0
	BEGIN
		DECLARE examcode_cursor CURSOR FOR 
			SELECT examcode FROM track WHERE 
				candidate=@candidate AND
				app_receive IS NOT NULL AND 
				sig_receive IS NOT NULL AND 
				app_approve IS NULL AND 
				(
					(exam='M' AND year=(select attr from mcodes where grp='AY' and code='M')) 
						OR 
					(type='R' AND year=(select attr from mcodes where grp='AY' and code='T'))
				)
		OPEN examcode_cursor
		FETCH NEXT FROM examcode_cursor INTO @examcode
		WHILE @@FETCH_STATUS = 0
		BEGIN
			EXECUTE ins_app_approve @candidate,@examcode
			FETCH NEXT FROM examcode_cursor INTO @examcode
		END
		CLOSE examcode_cursor
		DEALLOCATE examcode_cursor		
	END
END

IF EXISTS 
(select * from
	(
	   select  explain,official,coordinator,title,address,city,state,zip,email,coordinator_email,phone,hosp,let_rcvd,coordinator_rcvd,let_sent,let_approve,signature,coordinator_signature,pd_confirm,coordinator_confirm
	   			   from Inserted
	   except
	   select  explain,official,coordinator,title,address,city,state,zip,email,coordinator_email,phone,hosp,let_rcvd,coordinator_rcvd,let_sent,let_approve,signature,coordinator_signature,pd_confirm,coordinator_confirm
	   			   from Deleted
	) a)
OR 
(select count(*) from inserted) = 0
BEGIN
	insert into AppRefLet_hist select deleted.* from deleted
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - Chief; 1 - Chair', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppRefLet', @level2type = N'COLUMN', @level2name = N'created';

