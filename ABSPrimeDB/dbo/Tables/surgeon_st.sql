CREATE TABLE [dbo].[surgeon_st] (
    [id]            NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [created]       DATETIME       NOT NULL,
    [creator]       CHAR (3)       NOT NULL,
    [modified]      DATETIME       NULL,
    [modifier]      CHAR (3)       NULL,
    [modifications] SMALLINT       NOT NULL,
    [UserId]        INT            NULL,
    [candidate]     CHAR (6)       NOT NULL,
    [status_code]   VARCHAR (50)   NOT NULL,
    [start_date]    DATETIME       NULL,
    [end_date]      DATETIME       NULL,
    [note]          VARCHAR (1000) NULL,
    [st_val]        VARCHAR (8000) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_surgeon_st_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [NonClusteredIndexCandidateStatuscode]
    ON [dbo].[surgeon_st]([candidate] ASC, [status_code] ASC) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER tr_surgeon_st_update ON dbo.surgeon_st
FOR  INSERT,UPDATE
AS

DECLARE @ID AS numeric,@candidate AS char(6),@status_code AS varchar(50),@st_val AS varchar(8000)

IF (SELECT count(*) FROM inserted) = 0 
BEGIN
	RETURN
END

SELECT @ID=id,@candidate=candidate,@status_code=RTRIM(status_code),@st_val=RTRIM(st_val) FROM inserted;

IF ((SELECT ISNULL(st_val,'') FROM inserted)!=(SELECT ISNULL(st_val,'') FROM deleted) OR (SELECT count(*) FROM deleted) = 0) AND 
	@status_code='mbs_registry' AND 
	@st_val='true' AND
	(SELECT MIN(status) status FROM app_response WHERE app_id='57' AND candidate=@candidate GROUP BY candidate)=1 
BEGIN
	UPDATE track SET fpd_approve=getdate(),modifier='sta',modifications=modifications+1 WHERE candidate=@candidate AND type='B' AND fpd_receive IS NOT NULL AND fpd_approve IS NULL 
END

if @@error != 0
begin 
	RAISERROR('Unable to update table', 16, 1)
end