CREATE TABLE [dbo].[exam_score] (
    [id]                  NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [created]             DATE           NOT NULL,
    [creator]             CHAR (3)       NOT NULL,
    [modified]            DATE           NULL,
    [modifier]            CHAR (3)       NULL,
    [modifications]       SMALLINT       NOT NULL,
    [exam_id]             NUMERIC (18)   NOT NULL,
    [examiner_userid]     INT            NULL,
    [examiner]            CHAR (6)       NULL,
    [role]                CHAR (2)       NULL,
    [score]               NUMERIC (9, 3) NULL,
    [score_adjusted]      NUMERIC (9, 3) NULL,
    [case_1]              VARCHAR (6)    NULL,
    [case_1_score]        NUMERIC (9, 3) CONSTRAINT [DF_exam_score_case_1_score_1] DEFAULT ((0)) NULL,
    [comment_1]           VARCHAR (MAX)  NULL,
    [critical_fail_1]     BIT            NULL,
    [case_2]              VARCHAR (6)    NULL,
    [case_2_score]        NUMERIC (9, 3) CONSTRAINT [DF_exam_score_case_2_score_1] DEFAULT ((0)) NULL,
    [comment_2]           VARCHAR (MAX)  NULL,
    [critical_fail_2]     BIT            NULL,
    [case_3]              VARCHAR (6)    NULL,
    [case_3_score]        NUMERIC (9, 3) CONSTRAINT [DF_exam_score_case_3_score_1] DEFAULT ((0)) NULL,
    [comment_3]           VARCHAR (MAX)  NULL,
    [critical_fail_3]     BIT            NULL,
    [case_4]              VARCHAR (6)    NULL,
    [case_4_score]        NUMERIC (9, 3) CONSTRAINT [DF_exam_score_case_4_score_1] DEFAULT ((0)) NULL,
    [comment_4]           VARCHAR (MAX)  NULL,
    [critical_fail_4]     BIT            NULL,
    [cmp_num_case_scores] AS             (((case when isnull([CASE_1_SCORE],(0))>(0) then (1) else (0) end+case when isnull([CASE_2_SCORE],(0))>(0) then (1) else (0) end)+case when isnull([CASE_3_SCORE],(0))>(0) then (1) else (0) end)+case when isnull([CASE_4_SCORE],(0))>(0) then (1) else (0) end),
    [cmp_num_cases]       AS             (((case when len(rtrim(isnull([CASE_1],'')))>(0) then (1) else (0) end+case when len(rtrim(isnull([CASE_2],'')))>(0) then (1) else (0) end)+case when len(rtrim(isnull([CASE_3],'')))>(0) then (1) else (0) end)+case when len(rtrim(isnull([CASE_4],'')))>(0) then (1) else (0) end),
    [override_reason]     VARCHAR (1000) NULL,
    [override_ip]         VARCHAR (15)   NULL,
    CONSTRAINT [PK_exam_score] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_exam_score_users] FOREIGN KEY ([examiner_userid]) REFERENCES [Users]([UserId])
);


GO
CREATE CLUSTERED INDEX [exam_score_exam_id]
    ON [dbo].[exam_score]([exam_id] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_score_examiner]
    ON [dbo].[exam_score]([examiner] ASC) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER tr_exam_score_update ON dbo.exam_score 
	FOR  UPDATE, DELETE
AS

DECLARE @ID AS numeric,@ISUPDATE AS int,@modifier varchar(30)

SELECT @ISUPDATE=count(*) FROM inserted
IF  @ISUPDATE>0 
BEGIN
	SELECT @ID=id,@modifier= CASE isnull(modifier,'') WHEN '' THEN LEFT(APP_NAME(),3) ELSE RIGHT(modifier,3) END FROM inserted
	UPDATE exam_score set modified=getdate(), modifier=@modifier, modifications=modifications+1 where id=@ID
END

INSERT INTO exam_score_hist 
	SELECT deleted.* FROM deleted
