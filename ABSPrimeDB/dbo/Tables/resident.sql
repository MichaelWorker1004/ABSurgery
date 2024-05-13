CREATE TABLE [dbo].[resident] (
    [created]                [dbo].[created]       NOT NULL,
    [creator]                [dbo].[creator]       NOT NULL,
    [modified]               [dbo].[modified]      NULL,
    [modifier]               [dbo].[modifier]      NULL,
    [modifications]          [dbo].[modifications] NOT NULL,
    [exam]                   [dbo].[exam]          NULL,
    [current_year]           SMALLINT              NULL,
    [program]                CHAR (4)              NULL,
    [last_name]              VARCHAR (30)          NULL,
    [first_name]             VARCHAR (30)          NULL,
    [middle_name]            VARCHAR (30)          NULL,
    [ssn]                    [dbo].[ssn]           NULL,
    [level]                  TINYINT               NULL,
    [research]               CHAR (1)              NULL,
    [comp_year]              SMALLINT              NULL,
    [UserId]                 INT                   NULL,
    [candidate]              CHAR (6)              NULL,
    [id]                     NUMERIC (9)           IDENTITY (1, 1) NOT NULL,
    [pd_auth]                VARCHAR (100)         NULL,
    [roster_id]              NUMERIC (9)           NOT NULL,
    [notes]                  VARCHAR (1000)        NULL,
    [other]                  VARCHAR (1000)        NULL,
    [completed]              TINYINT               NULL,
    [continuing]             TINYINT               NULL,
    [gender]                 CHAR (1)              NULL,
    [birthdate]              SMALLDATETIME         NULL,
    [grad_year]              SMALLINT              NULL,
    [category]               TINYINT               CONSTRAINT [DF_resident_category] DEFAULT ((0)) NULL,
    [rtype]                  TINYINT               CONSTRAINT [DF_resident_rtype] DEFAULT ((0)) NULL,
    [uid]                    VARCHAR (13)          NULL,
    [pwd]                    VARCHAR (12)          NULL,
    [record_sent]            DATETIME              NULL,
    [fec]                    TINYINT               NULL,
    [istaking]               TINYINT               NULL,
    [accommodations]         VARCHAR (5)           NULL,
    [alternatesite]          VARCHAR (5)           NULL,
    [orderformclinicallevel] TINYINT               NULL,
    [orderformonly]          TINYINT               NULL,
    [req_met]                BIT                   NULL,
    [offcycle]               BIT                   NULL,
    [score]                  FLOAT (53)            NULL,
    CONSTRAINT [PK_resident] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_resident_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [resident_uc]
    ON [dbo].[resident]([ssn] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [resident_cs]
    ON [dbo].[resident]([last_name] ASC) WITH (FILLFACTOR = 90);


GO

CREATE TRIGGER [dbo].[tr_resident_update] ON [dbo].[resident]
FOR UPDATE ,INSERT, DELETE
AS

DECLARE @ID AS numeric,	@DELETED as int, @INSERTED as int , @roster_id AS numeric, @candidate as varchar(6)

SELECT @DELETED=count(*) from deleted
SELECT @INSERTED=count(*) from inserted

IF @INSERTED>0 
BEGIN
	SELECT @ID=id,@roster_id=roster_id,@candidate=RTRIM(ISNULL(candidate,'')) FROM inserted
END
ELSE
BEGIN
	SELECT @ID=id,@roster_id=roster_id,@candidate=RTRIM(ISNULL(candidate,'')) FROM deleted
END	

IF @INSERTED>0 AND @DELETED>0 AND 
	(SELECT CONVERT(VARBINARY(250),RTRIM(ISNULL(last_name,''))+RTRIM(ISNULL(first_name,''))+RTRIM(ISNULL(ssn,''))) FROM inserted where id=@ID)!= 
	(SELECT CONVERT(VARBINARY(250),RTRIM(ISNULL(last_name,''))+RTRIM(ISNULL(first_name,''))+RTRIM(ISNULL(ssn,''))) FROM deleted where id=@ID)
BEGIN
	UPDATE resident set resident.modifier=LEFT(APP_NAME(),3),resident.modified = getdate(),resident.modifications=resident.modifications+1
	FROM resident ,inserted
	WHERE resident.id = @ID
END
/*--disabling surgeon auto-update per GSD-973
IF @INSERTED>0 AND LEN(@candidate)=6 AND (SELECT COUNT(candidate) FROM surgeon WHERE candidate=@candidate)=1 --candidate number is valid and not duplicated
BEGIN
	IF	(
			(SELECT CONVERT(VARBINARY(250),RTRIM(ISNULL(last_name,''))+RTRIM(ISNULL(first_name,''))+RTRIM(ISNULL(middle_name,''))+RTRIM(ISNULL(ssn,''))) FROM inserted WHERE id=@ID)!= 
			(SELECT CONVERT(VARBINARY(250),RTRIM(ISNULL(last_name,''))+RTRIM(ISNULL(first_name,''))+RTRIM(ISNULL(middle_name,''))+RTRIM(ISNULL(ssn,''))) FROM surgeon WHERE candidate=@candidate)
				OR 
			@DELETED=0
		) 
	BEGIN
		UPDATE surgeon
		SET
		    surgeon.first_name = ISNULL(inserted.first_name,''),
		    surgeon.last_name = ISNULL(inserted.last_name,''),
		    surgeon.middle_name = ISNULL(inserted.middle_name,''),
		    surgeon.ssn = ISNULL(inserted.ssn,'')	    
		FROM surgeon,inserted
		WHERE inserted.id=@ID and surgeon.candidate = inserted.candidate
	END
END
*/
IF 	(@DELETED=0 AND (select isnull(istaking,0) from inserted where id=@ID) = 1) OR
	(@DELETED>0 AND @INSERTED>0 AND (select isnull(istaking,0) from inserted where id=@ID) != (select isnull(istaking,0) from deleted where id=@ID)) OR
	(@INSERTED=0 AND (select isnull(istaking,0) from deleted where id=@ID) = 1)
BEGIN
	UPDATE inv_det 
		SET inv_det.quantity = resident.quantity
	FROM inv_det
		INNER JOIN
			(
				select sum(isnull(istaking,0)) quantity, 'P'+number+roster.exam+cast(roster.current_year as varchar)+roster.exam+'TE' inv_num from resident 
				inner join roster on resident.roster_id=roster.id
				where roster_id=@roster_id
				group by 'P'+number+roster.exam+cast(roster.current_year as varchar)+roster.exam
			) resident
		ON inv_det.inv_num=	resident.inv_num
END

IF @@error != 0
BEGIN
	RAISERROR('Unable to update table', 16, 1)
END

