CREATE TABLE [dbo].[dscpl_action] (
    [id]            NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [created]       DATETIME       NOT NULL,
    [creator]       CHAR (3)       NOT NULL,
    [modified]      DATETIME       NULL,
    [modifier]      CHAR (3)       NULL,
    [modifications] SMALLINT       NOT NULL,
    [UserId]        INT            NULL,
    [candidate]     CHAR (6)       NOT NULL,
    [dscpl_code]    VARCHAR (25)   NOT NULL,
    [act_agency]    VARCHAR (50)   NULL,
    [act_entr]      DATETIME       NULL,
    [act_snct]      DATETIME       NULL,
    [act_init]      DATETIME       NULL,
    [act_followup]  DATETIME       NULL,
    [act_executed]  DATETIME       NULL,
    [due_back]      DATETIME       NULL,
    [effective]     TINYINT        NULL,
    [followup]      TINYINT        NULL,
    [reasons]       VARCHAR (1000) NULL,
    [conditions]    VARCHAR (1000) NULL,
    [note]          VARCHAR (1000) NULL,
    PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [FK_dscpl_action_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
    CONSTRAINT [IX_dscpl_action] UNIQUE CLUSTERED ([candidate] ASC, [dscpl_code] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE TRIGGER tr_dscpl_action_update ON dbo.dscpl_action
	FOR  UPDATE, DELETE
AS

DECLARE @ID AS numeric,@ISUPDATE as int,@modifier varchar(30)

SELECT @ISUPDATE=count(*) FROM inserted
IF  @ISUPDATE>0 
BEGIN
	SELECT @ID=id,@modifier= CASE isnull(modifier,'') WHEN '' THEN LEFT(APP_NAME(),3) ELSE RIGHT(modifier,3) END FROM inserted
	UPDATE dscpl_action set modified=getdate(), modifier=@modifier, modifications=modifications+1 where id=@ID
END
INSERT INTO dscpl_action_hist 
	SELECT deleted.* FROM deleted