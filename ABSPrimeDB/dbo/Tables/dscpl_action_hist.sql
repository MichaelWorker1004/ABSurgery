CREATE TABLE [dbo].[dscpl_action_hist] (
    [id]            NUMERIC (18)   NOT NULL,
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
    [note]          VARCHAR (1000) NULL
);

