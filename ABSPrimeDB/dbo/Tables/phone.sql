CREATE TABLE [dbo].[phone] (
    [created]       [dbo].[created]       NOT NULL,
    [creator]       [dbo].[creator]       NOT NULL,
    [modified]      [dbo].[modified]      NULL,
    [modifier]      [dbo].[modifier]      NULL,
    [modifications] [dbo].[modifications] NOT NULL,
    [code]          CHAR (6)              NOT NULL,
    [status]        CHAR (1)              NOT NULL,
    [type1]         CHAR (1)              NOT NULL,
    [type2]         CHAR (1)              NOT NULL,
    [number]        VARCHAR (100)         NULL,
    [note]          VARCHAR (255)         NULL,
    [id]            NUMERIC (9)           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_phone] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE UNIQUE CLUSTERED INDEX [phone_uc]
    ON [dbo].[phone]([code] ASC, [type1] ASC, [type2] ASC, [status] ASC) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER tr_phone_update ON dbo.phone
FOR  UPDATE
AS

DECLARE @ID AS numeric,@modifier varchar(30),@column_name varchar(30)

SELECT @modifier = CASE isnull(modifier,'') WHEN '' THEN LEFT(APP_NAME(),3) ELSE RIGHT(modifier,3) END FROM inserted
		
IF (SELECT count(*) FROM inserted) = 0 OR (SELECT count(*) FROM deleted) = 0
BEGIN
	RETURN
END


SELECT @ID=id,@column_name=case type2 when 'V' then 'phone' when 'F' then 'fax' when 'B' then 'email' else '' end FROM inserted

IF 	(SELECT number FROM inserted)!=(SELECT number FROM deleted) OR 
	(SELECT type1 FROM inserted)!=(SELECT type1 FROM deleted) OR
	(SELECT type2 FROM inserted)!=(SELECT type2 FROM deleted) OR 
	(SELECT note FROM inserted)!=(SELECT note FROM deleted) 	
BEGIN
	INSERT INTO phone_hist SELECT * FROM deleted;
END

IF (SELECT modified FROM inserted)=(SELECT modified FROM deleted)
BEGIN
	UPDATE phone SET modified=getdate(), modifier=@modifier, modifications=modifications+1 WHERE id=@ID
END


IF (SELECT number FROM inserted)!=(SELECT number FROM deleted) 
BEGIN
	IF EXISTS (SELECT * FROM lastupdated WHERE table_name='phone' AND table_id=@ID) 
	BEGIN
		UPDATE lastupdated SET lastupdated=getdate(),modifier=@modifier WHERE table_name='phone' AND table_id=@ID
	END
	ELSE
	BEGIN
		INSERT INTO lastupdated (table_name,column_name,table_id,lastupdated,modifier) SELECT 'phone',@column_name,@ID,getdate(),@modifier
	END
END

if @@error != 0
begin 
	RAISERROR('Unable to update table', 16, 1)
end