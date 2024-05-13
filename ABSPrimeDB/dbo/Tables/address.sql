CREATE TABLE [dbo].[address] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [type]          CHAR (1)              NOT NULL,
    [code]          CHAR (6)              NOT NULL,
    [status]        CHAR (1)              NOT NULL,
    [location]      CHAR (1)              NULL,
    [mail]          CHAR (1)              NULL,
    [title]         VARCHAR (50)          NULL,
    [institution]   VARCHAR (50)          NULL,
    [department]    VARCHAR (50)          NULL,
    [street1]       VARCHAR (100)         NULL,
    [street2]       VARCHAR (100)         NULL,
    [street3]       VARCHAR (50)          NULL,
    [street4]       VARCHAR (50)          NULL,
    [city]          VARCHAR (30)          NULL,
    [state]         CHAR (2)              NULL,
    [zip]           CHAR (10)             NULL,
    [note]          VARCHAR (255)         NULL,
    [id]            NUMERIC (9)           IDENTITY (1, 1) NOT NULL,
    [fedex]         BIT                   NULL,
    [DPV]           CHAR (1)              NULL,
    [DPVFootnote]   VARCHAR (15)          NULL,
    [County]        VARCHAR (50)          NULL,
    [RecordType]    CHAR (1)              NULL,
    [Latitude]      FLOAT (53)            NULL,
    [Longitude]     FLOAT (53)            NULL,
    [StatusNbr]     SMALLINT              NULL,
    [LastVerified]  SMALLDATETIME         NULL,
    CONSTRAINT [PK_address] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE UNIQUE CLUSTERED INDEX [address_uc]
    ON [dbo].[address]([code] ASC, [type] ASC, [status] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20220511-144838]
    ON [dbo].[address]([code] ASC, [mail] ASC);


GO
CREATE TRIGGER tr_address_update ON dbo.address
FOR  UPDATE ,INSERT
AS

DECLARE @ID as numeric,@code as char(6),@ISUPDATE as int,@modifier varchar(30)
		
if (select count(*) from inserted) = 0
begin
	return
end

select @ID=id,@code=code from inserted
select @modifier= case isnull(modifier,'') when '' then left(APP_NAME(),3) else right(modifier,3) end from inserted

if (select isnull(mail,'') from inserted where id=@ID)='M' and ((select isnull(mail,'') from deleted where id=@ID)!='M' or (select count(*) from deleted)=0)
begin
	insert into address_hist select * from address where code = @code and mail='M' and id!=@ID
	update address set mail = null,modified=getdate(), modifier=@modifier,modifications=modifications+1 where code = @code and mail='M' and id!=@ID
end

if (select fedex from inserted where id=@ID)=1 and ((select fedex from deleted where id=@ID)!=1 or (select count(*) from deleted)=0)
begin
	update address set fedex=0 where code = @code and id!=@ID and fedex=1
end

if (select isnull(mail,'') from inserted where id=@ID)='M'
begin
	delete from surgeon_st where candidate=@code and status_code='NM'
end

select @ISUPDATE=count(*) from deleted
if  @ISUPDATE>0 and EXISTS 
	(select inserted.id from inserted,deleted where inserted.id=deleted.id and 
		(isnull(inserted.mail,'')!=isnull(deleted.mail,'') or 
		isnull(inserted.title,'')!=isnull(deleted.title,'') or 
		isnull(inserted.institution,'')!=isnull(deleted.institution,'') or 
		isnull(inserted.department,'')!=isnull(deleted.department,'') or 
		isnull(inserted.street1,'')!=isnull(deleted.street1,'') or 
		isnull(inserted.street2,'')!=isnull(deleted.street2,'') or 
		isnull(inserted.street4,'')!=isnull(deleted.street4,'') or 
		isnull(inserted.city,'')!=isnull(deleted.city,'') or 
		isnull(inserted.state,'')!=isnull(deleted.state,'') or 
		isnull(inserted.zip,'')!=isnull(deleted.zip,'') or 
		isnull(inserted.fedex,0)!=isnull(deleted.fedex,0))
	)
begin
	update address set modified=getdate(), modifier=@modifier,modifications=modifications+1 where id=@ID
	insert into address_hist (created,creator,modified,modifier,modifications,type,code,status,location,mail,title,institution,department,street1,street2,street3,street4,city,state,zip,note,id,fedex)
		select created,creator,modified,modifier,modifications,type,code,status,location,mail,title,institution,department,street1,street2,street3,street4,city,state,zip,note,id,fedex from deleted where id=@ID
	
	IF EXISTS (SELECT * FROM lastupdated WHERE table_name='address' AND column_name='address' AND table_id=@ID) 
	BEGIN
		UPDATE lastupdated SET lastupdated=getdate(),modifier=@modifier WHERE table_name='address' AND column_name='address' AND table_id=@ID
	END
	ELSE
	BEGIN
		INSERT INTO lastupdated (table_name,column_name,table_id,lastupdated,modifier) SELECT 'address','address',@ID,getdate(),@modifier
	END			
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'org1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'address', @level2type = N'COLUMN', @level2name = N'title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'org2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'address', @level2type = N'COLUMN', @level2name = N'institution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'not used', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'address', @level2type = N'COLUMN', @level2name = N'department';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'not used', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'address', @level2type = N'COLUMN', @level2name = N'street3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'country', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'address', @level2type = N'COLUMN', @level2name = N'street4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'address', @level2type = N'COLUMN', @level2name = N'state';

