CREATE TABLE [dbo].[moc_eligibility] (
    [UserId]                   INT              NULL,
    [candidate]                CHAR (6)         NOT NULL,
    [created]                  [dbo].[created]  NULL,
    [creator]                  [dbo].[creator]  NULL,
    [modified]                 [dbo].[modified] NULL,
    [modifier]                 [dbo].[modifier] NULL,
    [cohort]                   SMALLINT         NULL,
    [examcode]                 VARCHAR (7)      NULL,
    [seniority_date]           SMALLDATETIME    NULL,
    [cmoc_cohort]              SMALLINT         NULL,
    [cmoc_examcode]            VARCHAR (7)      NULL,
    [cmoc_seniority_date]      SMALLDATETIME    NULL,
    [optout]                   SMALLDATETIME    NULL,
    [current_cycle]            AS               ((([dbo].[udf_get_academic_year]('M')-[cohort])-(1))/(5)),
    [current_cycle_year]       AS               ((([dbo].[udf_get_academic_year]('M')-[cohort])-(1))%(5)+(1)),
    [current_year]             AS               ([dbo].[udf_get_current_year]([cohort])),
    [current_trackcode]        AS               (CONVERT([varchar],[dbo].[udf_get_current_year]([cohort]),(0))+'MC'),
    [current_cycle_due]        AS               (CONVERT([smalldatetime],CONVERT([varchar],[dbo].[udf_get_current_year]([cohort]),(0))+'1231',(121))),
    [next_reporting_due]       AS               ([dbo].[udf_get_moc_reporting_due_v2]([candidate])),
    [extension_due]            DATETIME         NULL,
    [num_cycles_non_compliant] AS               ([dbo].[udf_get_moc_cycles_non_compliant]([candidate])),
    [note]                     VARCHAR (1000)   NULL,
    CONSTRAINT [PK_moc_eligibility] PRIMARY KEY NONCLUSTERED ([candidate] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_moc_eligibility_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [IX_moc_eligibility_1]
    ON [dbo].[moc_eligibility]([cohort] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX ['nc_examcode']
    ON [dbo].[moc_eligibility]([examcode] ASC) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER [dbo].[tr_moc_eligibility_update] ON dbo.moc_eligibility
FOR  UPDATE ,DELETE
AS

DECLARE @ISUPDATE as int,@modifier varchar(30),@candidate char(6)

select @ISUPDATE=count(*) from inserted
if  @ISUPDATE>0
begin
	select @modifier= case isnull(modifier,'') when '' then left(APP_NAME(),3) else right(modifier,3) end, @candidate=candidate from inserted
	update moc_eligibility set modified=getdate(), modifier=@modifier where candidate=@candidate
end
insert into moc_eligibility_hist
	select deleted.*, req_cme_1 int, req_cme_1_sa int,  req_cme_tot int, cme_from datetime, cme_to datetime
	from deleted
	left join track on deleted.candidate=track.candidate and deleted.current_trackcode=track.examcode
