CREATE TABLE [dbo].[surgeon] (
    [created]           [dbo].[created]       NOT NULL,
    [creator]           [dbo].[creator]       NOT NULL,
    [modified]          [dbo].[modified]      NULL,
    [modifier]          [dbo].[modifier]      NULL,
    [modifications]     [dbo].[modifications] NOT NULL,
    [UserId]            INT                   NULL,
    [candidate]         CHAR (6)              NOT NULL,
    [ssn]               [dbo].[ssn]           NULL,
    [status]            CHAR (1)              NOT NULL,
    [degree]            CHAR (1)              NULL,
    [first_name]        VARCHAR (30)          NULL,
    [middle_name]       VARCHAR (30)          NULL,
    [last_name]         VARCHAR (30)          NOT NULL,
    [suffix]            VARCHAR (20)          NULL,
    [race]              VARCHAR (20)          NULL,
    [sex]               VARCHAR (20)          NULL,
    [birthdate]         DATETIME              NULL,
    [birthplace]        CHAR (3)              NULL,
    [citizen]           CHAR (1)              NULL,
    [country]           CHAR (3)              NULL,
    [school]            CHAR (1)              NULL,
    [year]              SMALLINT              NULL,
    [note]              TEXT                  NULL,
    [Canadian]          CHAR (1)              NULL,
    [dispname]          VARCHAR (100)         NULL,
    [pwd]               VARCHAR (100)         NULL,
    [pwd_reset]         BIT                   NULL,
    [pwd_bak]           VARCHAR (12)          NULL,
    [uid]               VARCHAR (12)          NULL,
    [licstate]          VARCHAR (2)           NULL,
    [licdesc]           VARCHAR (1000)        NULL,
    [practicedesc]      VARCHAR (8000)        NULL,
    [licnum]            VARCHAR (10)          NULL,
    [licexp]            DATETIME              NULL,
    [npi]               VARCHAR (10)          NULL,
    [pwd_last_modified] DATETIME              NULL,
    [id]                INT                   IDENTITY (136549, 1) NOT FOR REPLICATION NOT NULL,
    [acct_num]          AS                    (([candidate]+'A')+CONVERT([char](6),[id],(0))),
    [ethnicity]         CHAR (1)              NULL,
    CONSTRAINT [PK_surgeon] PRIMARY KEY NONCLUSTERED ([candidate] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_surgeon_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [surgeon_last_name]
    ON [dbo].[surgeon]([last_name] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [surgeon_first_name]
    ON [dbo].[surgeon]([first_name] ASC) WITH (FILLFACTOR = 90);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_7_6_surgeon]
    ON [dbo].[surgeon]([id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_197_196_surgeon]
    ON [dbo].[surgeon]([candidate] ASC)
    INCLUDE([uid]);


GO
CREATE NONCLUSTERED INDEX [IX_21151_21150_surgeon]
    ON [dbo].[surgeon]([candidate] ASC)
    INCLUDE([ssn], [birthdate], [uid]);


GO
CREATE TRIGGER tr_surgeon_update ON dbo.surgeon
FOR  UPDATE
AS

DECLARE @ID AS numeric,@modifier varchar(30)

SELECT @modifier = CASE isnull(modifier,'') WHEN '' THEN LEFT(APP_NAME(),3) ELSE RIGHT(modifier,3) END FROM inserted
		
IF (SELECT count(*) FROM inserted) = 0 OR (SELECT count(*) FROM deleted) = 0
BEGIN
	RETURN
END

SELECT @ID=id FROM inserted

IF (SELECT first_name FROM inserted)!=(SELECT first_name FROM deleted) or (SELECT middle_name FROM inserted)!=(SELECT middle_name FROM deleted) or (SELECT last_name FROM inserted)!=(SELECT last_name FROM deleted)
BEGIN
	IF EXISTS (SELECT * FROM lastupdated WHERE table_name='surgeon' AND column_name='name' AND table_id=@ID) 
	BEGIN
		UPDATE lastupdated SET lastupdated=getdate(),modifier=@modifier WHERE table_name='surgeon' AND column_name='name' AND table_id=@ID
	END
	ELSE
	BEGIN
		INSERT INTO lastupdated (table_name,column_name,table_id,lastupdated,modifier) SELECT 'surgeon','name',@ID,getdate(),@modifier
	END
END

IF (SELECT degree FROM inserted)!=(SELECT degree FROM deleted) or (SELECT year FROM inserted)!=(SELECT year FROM deleted)
BEGIN
	IF EXISTS (SELECT * FROM lastupdated WHERE table_name='surgeon' AND column_name='education' AND table_id=@ID) 
	BEGIN
		UPDATE lastupdated SET lastupdated=getdate(),modifier=@modifier WHERE table_name='surgeon' AND column_name='education' AND table_id=@ID
	END
	ELSE
	BEGIN
		INSERT INTO lastupdated (table_name,column_name,table_id,lastupdated,modifier) SELECT 'surgeon','education',@ID,getdate(),@modifier
	END
END

if @@error != 0
begin 
	RAISERROR('Unable to update table', 16, 1)
end

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'license description/other', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'surgeon', @level2type = N'COLUMN', @level2name = N'licdesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'description of sugical practice', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'surgeon', @level2type = N'COLUMN', @level2name = N'practicedesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'national provider id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'surgeon', @level2type = N'COLUMN', @level2name = N'npi';

