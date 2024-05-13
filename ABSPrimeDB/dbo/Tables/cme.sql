CREATE TABLE [dbo].[cme] (
    [id]                    NUMERIC (10)   IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UserId]                INT            NULL,
    [candidate]             CHAR (6)       NOT NULL,
    [created]               SMALLDATETIME  NULL,
    [ReportingOrganization] VARCHAR (300)  CONSTRAINT [DF_cme_ReportingOrganization] DEFAULT ('SELF') NULL,
    [inv_num]               VARCHAR (16)   NULL,
    [org_id]                VARCHAR (16)   NULL,
    [FirstName]             VARCHAR (30)   NULL,
    [MiddleName]            VARCHAR (30)   NULL,
    [LastName]              VARCHAR (30)   NULL,
    [Email]                 VARCHAR (100)  NULL,
    [ProviderOrganization]  VARCHAR (300)  NULL,
    [ActivityName]          VARCHAR (1000) NULL,
    [ActivityID]            NUMERIC (10)   NULL,
    [ModuleName]            VARCHAR (1000) NULL,
    [ModuleID]              NUMERIC (10)   NULL,
    [AccreditingBody]       VARCHAR (300)  NULL,
    [AccreditedProvider]    VARCHAR (300)  NULL,
    [NonAccreditedProvider] VARCHAR (300)  NULL,
    [ReleaseDate]           VARCHAR (10)   NOT NULL,
    [Hours]                 NUMERIC (9, 2) NOT NULL,
    [Verified]              VARCHAR (3)    NULL,
    [SelfAssessment]        VARCHAR (3)    NULL,
    [CreditID]              VARCHAR (50)   NULL,
    [CreditID_SA]           VARCHAR (50)   NULL,
    [FileName]              VARCHAR (100)  NULL,
    [FileDate]              DATETIME       NULL,
    [Editable]              BIT            CONSTRAINT [DF_cme_Editable] DEFAULT ((1)) NULL,
    [cmecategory]           VARCHAR (1)    NULL,
    [usedwith]              VARCHAR (7)    CONSTRAINT [DF_cme_usedwith] DEFAULT ('true') NULL,
    [usedwith_app]          AS             (case when len([usedwith])=(6) then [usedwith] else '' end),
    [ReleaseDate_new]       DATE           NULL,
    [credits_sa]            NUMERIC (9, 2) CONSTRAINT [DF_cme_credits_sa] DEFAULT ((0)) NULL,
    [isPars]                AS             (case when isnull([ModuleID],(0))=(0) then 'No' else 'Yes' end),
    [CreatedByUserId]       INT            NULL,
    [CreatedAtUtc]          SMALLDATETIME  NULL,
    [credit_exp_date]       DATE           NULL,
    [LastUpdatedAtUtc] DATETIME NOT NULL DEFAULT GetUtcDate(),
    [LastUpdatedByUserId]   INT             NULL,
    CONSTRAINT [PK_cme] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_cme_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [IX_cme]
    ON [dbo].[cme]([candidate] ASC, [ReleaseDate] DESC, [ReportingOrganization] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [NC_RepOrg_InvNum_Cme]
    ON [dbo].[cme]([ReportingOrganization] ASC, [inv_num] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [NC_CreditID]
    ON [dbo].[cme]([CreditID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_13990_13989_cme]
    ON [dbo].[cme]([candidate] ASC, [ReportingOrganization] ASC)
    INCLUDE([inv_num], [ActivityName], [ReleaseDate], [Hours]);


GO
CREATE NONCLUSTERED INDEX [IX_46924_46923_cme]
    ON [dbo].[cme]([ReportingOrganization] ASC, [created] ASC)
    INCLUDE([id], [candidate], [inv_num], [org_id], [FirstName], [MiddleName], [LastName], [Email], [ProviderOrganization], [ActivityName], [ActivityID], [ModuleName], [ModuleID], [AccreditingBody], [AccreditedProvider], [NonAccreditedProvider], [ReleaseDate], [Hours], [Verified], [SelfAssessment], [CreditID], [CreditID_SA], [FileName], [FileDate], [Editable], [cmecategory], [usedwith], [usedwith_app], [credits_sa], [isPars]);


GO
CREATE NONCLUSTERED INDEX [IX_21068_21067_cme]
    ON [dbo].[cme]([candidate] ASC, [ReportingOrganization] ASC, [Verified] ASC)
    INCLUDE([inv_num], [ReleaseDate], [credits_sa]);


GO
CREATE NONCLUSTERED INDEX [IX_21064_21063_cme]
    ON [dbo].[cme]([candidate] ASC, [ReportingOrganization] ASC, [Verified] ASC)
    INCLUDE([inv_num], [ReleaseDate], [Hours]);


GO
CREATE NONCLUSTERED INDEX [IX_21062_21061_cme]
    ON [dbo].[cme]([candidate] ASC, [Verified] ASC, [ReportingOrganization] ASC)
    INCLUDE([inv_num], [ReleaseDate], [Hours]);


GO
CREATE NONCLUSTERED INDEX [IX_21066_21065_cme]
    ON [dbo].[cme]([candidate] ASC, [Verified] ASC, [ReportingOrganization] ASC)
    INCLUDE([inv_num], [ReleaseDate], [credits_sa]);


GO
CREATE NONCLUSTERED INDEX [IX_21060_21059_cme]
    ON [dbo].[cme]([candidate] ASC, [ReportingOrganization] ASC, [Verified] ASC)
    INCLUDE([inv_num], [ReleaseDate], [credits_sa]);


GO
CREATE NONCLUSTERED INDEX [IX_21058_21057_cme]
    ON [dbo].[cme]([candidate] ASC, [ReportingOrganization] ASC, [Verified] ASC)
    INCLUDE([inv_num], [ReleaseDate], [Hours]);


GO
CREATE NONCLUSTERED INDEX [candidate_ReportingOrganization_Includes]
    ON [dbo].[cme]([candidate] ASC, [ReportingOrganization] ASC)
    INCLUDE([inv_num], [ActivityName], [ReleaseDate], [Hours]);


GO
CREATE TRIGGER tr_cme_insert ON dbo.cme
FOR  INSERT
AS

DECLARE @ID as numeric
		
if (select count(*) from inserted) = 0
begin
	return
end

select @ID=id from inserted

if (select ReportingOrganization from inserted where id=@ID) is NULL
begin
	UPDATE cme SET ReportingOrganization='SELF'  WHERE id=@id
end

if (select cmecategory from inserted where id=@ID) is NULL
begin
	UPDATE cme SET cmecategory='1'  WHERE id=@id
end

if (select verified from inserted where id=@ID) is NULL
begin
	if 	(select ReportingOrganization from inserted where id=@ID) is NULL
		or
		(select ReportingOrganization from inserted where id=@ID) = 'SELF'
		or
		(
			(select ReportingOrganization from inserted where id=@ID)='ACS' and (select AccreditedProvider from inserted where id=@ID) not in ('ACS','JACSCME','JACS-CME')
		)
	begin
		UPDATE cme SET verified='No'  WHERE id=@id
	end
	else
	begin
		UPDATE cme SET verified='Yes'  WHERE id=@id
	end
end

if (select selfassessment from inserted where id=@ID) is NULL
begin
	UPDATE cme SET selfassessment='No'  WHERE id=@id
end

if (select created from inserted where id=@ID) is NULL
begin
	UPDATE cme SET created=getdate()  WHERE id=@id
end

if (select usedwith from inserted where id=@ID) is NULL
begin
	UPDATE cme SET usedwith='true'  WHERE id=@id
end
