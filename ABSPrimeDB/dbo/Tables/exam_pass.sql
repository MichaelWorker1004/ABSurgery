CREATE TABLE [dbo].[exam_pass] (
    [created]             [dbo].[created]       NOT NULL,
    [creator]             [dbo].[creator]       NOT NULL,
    [modified]            [dbo].[modified]      NULL,
    [modifier]            [dbo].[modifier]      NULL,
    [modifications]       [dbo].[modifications] NOT NULL,
    [UserId]              INT                   NULL,
    [candidate]           CHAR (6)              NOT NULL,
    [exam]                [dbo].[exam]          NOT NULL,
    [ExamSpecialtyId]     INT                   NULL,
    [type]                CHAR (1)              NOT NULL,
    [ExamTypeId]          INT                   NULL,
    [ExamHeaderId]        INT                   NULL,
    [date]                SMALLDATETIME         NULL,
    [certificate]         CHAR (6)              NULL,
    [expiration]          SMALLINT              NULL,
    [id]                  INT                   IDENTITY (1, 1) NOT NULL,
    [abms_sent]           SMALLDATETIME         NULL,
    [isactive]            AS                    (isnull(sign(datediff(day,getdate(),CONVERT([datetime],'12/31/'+CONVERT([varchar],[expiration],(0)),(0)))),(0))),
    [exam_id]             NUMERIC (9)           NULL,
    [module]              TINYINT               NULL,
    [durationtype]        VARCHAR (1)           NULL,
    [durationtype_order]  AS                    (case isnull([durationtype],'') when 'L' then (3) when 'C' then (2) when 'T' then (1) else (0) end),
    [reverification_date] AS                    (case when [expiration]<datepart(year,getdate()) then '' when [durationtype]='C' AND [expiration]<[dbo].[udf_get_academic_year]('R') then CONVERT([varchar](4),[expiration]+(1),(0))+'-01-05' when [durationtype]='C' then CONVERT([varchar](4),[dbo].[udf_get_academic_year]('R')+(1),(0))+'-01-05' else '' end),
    [cc_diploma_ordered]  VARCHAR (1000)        NULL,
    CONSTRAINT [PK__exam_pas__3213E83E66F42B53] PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [FK_exam_pass_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [FK_exam_pass_examspecialtyid] FOREIGN KEY ([ExamSpecialtyId]) REFERENCES [exam_specialties]([Id]),
    CONSTRAINT [FK_exam_pass_examtypeid] FOREIGN KEY ([ExamTypeId]) REFERENCES [exam_types]([Id]),
    CONSTRAINT [FK_exam_pass_examheaderid] FOREIGN KEY ([ExamHeaderId]) REFERENCES [Exam_Directory]([Id])
);


GO
CREATE CLUSTERED INDEX [exam_pass_uc]
    ON [dbo].[exam_pass]([candidate] ASC, [exam] ASC, [type] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_pass_cert]
    ON [dbo].[exam_pass]([certificate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_pass_certtype]
    ON [dbo].[exam_pass]([certificate] ASC, [type] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_exam_pass_cert]
    ON [dbo].[exam_pass]([certificate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_exam_pass_certtype]
    ON [dbo].[exam_pass]([certificate] ASC, [type] ASC);


GO
CREATE TRIGGER [dbo].[tr_exam_pass_update] ON dbo.exam_pass
FOR  UPDATE
AS

DECLARE @ISUPDATE as int,@modifier varchar(30)
		
if (select count(*) from inserted) = 0
begin
	return
end

select @ISUPDATE=count(*) from deleted
select @modifier= case isnull(modifier,'') when '' then left(APP_NAME(),3) else right(modifier,3) end from inserted

if  @ISUPDATE>0 
begin
	 update exam_pass set modified=getdate(),modifier=@modifier,modifications=modifications+1
	 	where id IN (select id from inserted)
end
