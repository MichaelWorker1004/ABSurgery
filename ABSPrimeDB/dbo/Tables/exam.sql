CREATE TABLE [dbo].[exam] (
    [created]                    [dbo].[created]       NOT NULL,
    [creator]                    [dbo].[creator]       NOT NULL,
    [modified]                   [dbo].[modified]      NULL,
    [modifier]                   [dbo].[modifier]      NULL,
    [modifications]              [dbo].[modifications] NOT NULL,
	[UserId]        			 INT            	   NULL,
    [candidate]                  CHAR (6)              NOT NULL,
    [examHeaderId]               INT                   NULL,
    [exam]                       [dbo].[exam]          NOT NULL,
	[ExamSpecialtyId]			 INT				   NULL,
    [type]                       CHAR (1)              NOT NULL,
	[ExamTypeId]				 INT				   NULL,
	[ExamFormatId]				 INT				   NULL,
    [year]                       SMALLINT              NOT NULL,
    [date]                       SMALLDATETIME         NULL,
    [rply_sent]                  SMALLDATETIME         NULL,
    [rply_rcvd]                  SMALLDATETIME         NULL,
    [fee_rcvd]                   SMALLDATETIME         NULL,
    [fee_bounce]                 SMALLDATETIME         NULL,
    [adms_sent]                  SMALLDATETIME         NULL,
    [adms_rcvd]                  SMALLDATETIME         NULL,
    [fee_mony_rcvd]              MONEY                 NULL,
    [number]                     TINYINT               NULL,
    [status]                     CHAR (1)              NOT NULL,
	[ExamStatusId]				 INT				   NULL,
    [cardcode]                   CHAR (1)              NULL,
    [grp_cod]                    CHAR (1)              NULL,
    [area]                       CHAR (1)              NOT NULL,
    [center]                     CHAR (3)              NULL,
    [day]                        CHAR (1)              NULL,
    [briefing]                   CHAR (1)              NULL,
    [session]                    CHAR (1)              NULL,
    [result]                     CHAR (1)              NULL,
    [cecode]                     CHAR (1)              NULL,
    [note]                       VARCHAR (8000)        NULL,
    [pref1]                      VARCHAR (1)           NULL,
    [pref2]                      VARCHAR (1)           NULL,
    [pref3]                      VARCHAR (1)           NULL,
    [pref4]                      VARCHAR (1)           NULL,
    [pref5]                      VARCHAR (1)           NULL,
    [pref6]                      VARCHAR (1)           NULL,
    [pref_rcvd]                  VARCHAR (22)          NULL,
    [asgn_sent]                  SMALLDATETIME         NULL,
    [attempt]                    TINYINT               NULL,
    [id]                         NUMERIC (9)           IDENTITY (1, 1) NOT NULL,
    [timecode]                   SMALLINT              NULL,
    [vote]                       BIT                   CONSTRAINT [DF_exam_vote] DEFAULT ((0)) NULL,
    [result_comment]             VARCHAR (1000)        NULL,
    [pearson_sent]               SMALLDATETIME         NULL,
    [conf_override]              VARCHAR (15)          NULL,
    [pearson_auth_id]            NUMERIC (9)           NULL,
    [asgn_mod]                   SMALLDATETIME         NULL,
    [istaking]                   TINYINT               NULL,
    [fsmb_sent]                  SMALLDATETIME         NULL,
    [fsmb_action]                VARCHAR (1)           NULL,
    [examcode]                   AS                    (((CONVERT([varchar],[year],0)+[exam])+[type])+[area]),
    [pearson_regid]              VARCHAR (9)           NULL,
    [trackcode]                  AS                    ((CONVERT([varchar],[year],(0))+[exam])+[dbo].[udf_type_et]([exam],[type])),
    [pearson_SiteID]             INT                   NULL,
    [pearson_DateOfRegistration] SMALLDATETIME         NULL,
    [pearson_ExamState]          VARCHAR (11)          NULL,
    [module]                     TINYINT               NULL,
    [score]                      FLOAT (53)            NULL,
    [score_initial]              FLOAT (53)            NULL,
    CONSTRAINT [PK_exam] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
	CONSTRAINT [FK_exam_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
	CONSTRAINT [FK_exam_examspecialtyid] FOREIGN KEY ([ExamSpecialtyId]) REFERENCES [exam_specialties]([Id]),
	CONSTRAINT [FK_exam_examtypeid] FOREIGN KEY ([ExamTypeId]) REFERENCES [exam_types]([Id]),
	CONSTRAINT [FK_exam_examformatid] FOREIGN KEY ([ExamFormatId]) REFERENCES [exam_formats]([Id]),
	CONSTRAINT [FK_exam_examheaderid] FOREIGN KEY ([examHeaderId]) REFERENCES [Exam_Directory]([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [exam_uc]
    ON [dbo].[exam]([candidate] ASC, [exam] ASC, [type] ASC, [year] ASC, [area] ASC, [attempt] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_cc]
    ON [dbo].[exam]([exam] ASC, [type] ASC, [year] ASC, [center] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_cd]
    ON [dbo].[exam]([exam] ASC, [type] ASC, [date] ASC, [center] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX ['nc_examcode']
    ON [dbo].[exam]([examcode] ASC)
    INCLUDE([id]) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_exam_cd]
    ON [dbo].[exam]([exam] ASC, [type] ASC, [date] ASC, [center] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_exam_cc]
    ON [dbo].[exam]([exam] ASC, [type] ASC, [year] ASC, [center] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_exam_status_Includes_cand_exam_type_year]
    ON [dbo].[exam]([status] ASC)
    INCLUDE([candidate], [exam], [type], [year]);


GO
CREATE NONCLUSTERED INDEX [IX_status_Includes_covering]
    ON [dbo].[exam]([status] ASC)
    INCLUDE([candidate], [exam], [type], [year], [date], [area], [day], [result], [attempt], [id], [pearson_sent], [pearson_auth_id], [examcode], [pearson_ExamState], [module]);


GO
--drop TRIGGER tr_exam_delete ON exam
CREATE TRIGGER tr_exam_delete ON dbo.exam
FOR  DELETE 
AS

DECLARE @ID as numeric, @activity varchar(60)

select @ID=id from deleted

if (select count(*) from inserted) > 0 or @ID is null  --update
begin
	return
end
		
begin
	set @activity=(SELECT 'Deleted: EXAM '+candidate+' exam='+exam+type+cast(year as varchar(4))+area+cast(isnull(attempt,0) as char(1))+' id='+cast(@ID as varchar(6)) from deleted where id=@ID)
	execute ins_log @activity
end

GO
CREATE TRIGGER tr_exam_update ON dbo.exam
FOR  UPDATE ,INSERT
AS

DECLARE @ID as numeric,@ISUPDATE as int, 		@activity as varchar(60),@log_data as varchar(8000),@msg varchar(1000),@modifier varchar(30),@examcode varchar(7),@candidate varchar(6),@UserId int,@year int,@exam char(1),@type char(1),@area varchar(1),@cc_status int
		
if (select count(*) from inserted) = 0
begin
	return
end

select @ISUPDATE=count(*) from deleted

select @ID=id,@examcode=RTRIM(examcode),@candidate=candidate,@UserId=UserId,@modifier=case isnull(modifier,'') when '' then left(APP_NAME(),3) else right(modifier,3) end,@year=year,@exam=exam,@type=type,@area=RTRIM(ISNULL(area,'')) from inserted

if  @ISUPDATE>0 and (select cast(year as varchar)+rtrim(area) from inserted where id=@ID)!=(select cast(year as varchar)+rtrim(area) from deleted where id=@ID)  	and (select status from inserted where id=@ID)='T' 
	and (select rply_sent from inserted where id=@ID) is not null
begin
	update exam set status='P',area=(select rtrim(area) from deleted where id=@ID) where id=@ID

	insert into exam (candidate,status,area,exam,type,year,creator,asgn_mod) select candidate,'T',rtrim(area),exam,type,year,'trg',getdate() from inserted where id=@ID
end
else if ((select status from inserted where id=@ID)!=(select status from deleted where id=@ID) or @ISUPDATE=0) and (select status from inserted where id=@ID) in ('T','R')
begin
	 update exam set status=case status when 'T' then 'P' else 'N' end,
	 	modified=getdate(),modifier='trg' where id !=@ID and status in ('T','R') and exam+type+candidate=(select exam+type+candidate from inserted where id=@ID)
end

 if (select dbo.udf_get_examavail(@examcode,1))<=0 and (select status from inserted where id=@ID) in ('T','R')
 begin
 	if not exists(select attr from mcodes where code=@examcode and grp='EL')
	begin
		insert into mcodes (code,grp,descr) values (@examcode,'EL',getdate())
	end
 end
 
if (
	SELECT (SELECT floor(sum(cast(attr2 as int))*0.3) FROM mcodes WHERE code=@examcode AND grp='MA') - count(*) FROM
		(SELECT DISTINCT reexaminee.candidate FROM exam
			LEFT JOIN exam reexaminee ON reexaminee.candidate=exam.candidate AND reexaminee.exam=exam.exam AND reexaminee.type=exam.type AND reexaminee.year!=exam.year AND reexaminee.result in ('P','F')
			WHERE exam.examcode=@examcode AND exam.status IN ('T','R') AND reexaminee.examcode IS NOT NULL) a	
)<=0 
begin
	if not exists(select attr from mcodes where code=@examcode and grp='RL')
	begin
		insert into mcodes (code,grp,descr) values (@examcode,'RL',getdate())
	end
end

if (select cast(year as varchar)+rtrim(area) from inserted where id=@ID)!=(select cast(year as varchar)+rtrim(area) from deleted where id=@ID) or @ISUPDATE=0 or
	((select status from inserted where id=@ID)!=(select status from deleted where id=@ID) and (select status from inserted where id=@ID) in ('T','W'))
begin
	update exam set asgn_mod=getdate() where id = @ID
end

if (select status from inserted where id=@ID)!=(select status from deleted where id=@ID) and (select status from inserted where id=@ID) in ('W')
begin
	delete from exam where status ='W' and id in (select id from exam where id!=@ID and exam+type+candidate=(select exam+type+candidate from inserted where id=@ID))
end

update exam set modifier=@modifier,modified = getdate(),modifications=modifications+1 from exam where id=@ID

if  @ISUPDATE=0 or 
	(select @examcode+status+cast(isnull(attempt,0) as varchar) from inserted where id=@ID)!=(select @examcode+status+cast(isnull(attempt,0) as varchar) from deleted where id=@ID)
begin
	set @log_data=''
	DECLARE log_cursor CURSOR FOR 
		select isnull(msg,'') from (
		select 'Changed'+cast(id as varchar)+candidate+@examcode+cast(isnull(attempt,0) as varchar)+status+cast(created as varchar) msg from deleted exam where Id=@id 
			union
		select 'Stats'      +cast(id as varchar)+candidate+@examcode+cast(isnull(attempt,0) as varchar)+status+cast(created as varchar) msg from exam where candidate=
			(select candidate from inserted where id=@id)
		) info
	
	OPEN log_cursor
	FETCH NEXT FROM log_cursor INTO @msg
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @log_data=@log_data+'<info>'+@msg+'</info>'
		FETCH NEXT FROM log_cursor INTO @msg
	END
	
	CLOSE log_cursor
	DEALLOCATE log_cursor

	set @log_data='<log>'+@log_data+'</log>'

	select @activity='Exam ID'+cast(id as varchar)+' '+candidate+' '+@examcode+cast(isnull(attempt,0) as varchar) from inserted where id=@id
	execute ins_log @activity,@log_data,@modifier
end

if (select status from inserted where id=@ID)='T' and (@ISUPDATE=0 or (select status from deleted where id=@ID)!='T')
	and (@type!='R' OR @exam='Z' OR @area='')
begin
	if not exists (select inv_num from invoice where inv_num=cast(@UserId as varchar)+@examcode+'E')
	begin		
		insert into invoice (creator,UserId,inv_num,type,ref_id) VALUES 
							('exm',@UserId,cast(@UserId as varchar)+@examcode+'E','E',@ID)
	end
	else
	begin
		update invoice set ref_id=@ID where inv_num=cast(@UserId as varchar)+@examcode+'E'
	end

	delete from inv_det where inv_num=cast(@UserId as varchar)+@examcode+'E' and quantity!=0
	insert into inv_det (creator,UserId,inv_num,code,amount,quantity) 
		select 'exm',
			@UserId,
			cast(@UserId as varchar)+@examcode+'E',
			case 
				when isnull(c.code,'')!='' then c.code 
				when isnull(a.code,'')!='' then a.code else b.code 
			end,
			cast(isnull(c.attr,isnull(a.attr,isnull(b.attr,0))) as money),1 
		from inserted z
			left outer join mcodes a on left(a.code,3)=z.exam+z.type+'E' and a.grp='FC' and a.attr2='1' 
			left outer join mcodes b on b.code=z.exam+z.type+'ET' and b.grp='FC' and b.attr2='1'
			left outer join mcodes c on c.code=z.exam+z.type+z.area+'ET'+IIF(@type='R',dbo.udf_get_academic_year('M'),'') and c.grp='FC' and c.attr2='1'
		where z.id=@ID
	
		/*SELECT @cc_status=cc_status FROM dbo.udf_get_cc_req(@candidate);
	IF CHARINDEX('R', @examcode)>0 AND @cc_status IN (7,8,4) AND  
		EXISTS (SELECT exam FROM exam WHERE candidate=@candidate AND LEFT(examcode,6)=cast(@year-1 as char(4))+@exam+@type AND status='C')
	BEGIN
		INSERT INTO inv_det (creator,inv_num,code,amount,quantity) 
			SELECT 'exm', z.candidate+z.examcode+'E', a.code, cast(isnull(a.attr,0) as money), 1 
			FROM inserted z	
				INNER JOIN mcodes a ON code='DCCR' AND a.grp='FC' AND a.attr2='1' 	
	END*/

end

if @@error != 0
begin 
	RAISERROR('Unable to update table', 16, 1)
end
