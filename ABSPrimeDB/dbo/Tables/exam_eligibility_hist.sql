CREATE TABLE [dbo].[exam_eligibility_hist] (
    [created]       DATETIME NULL,
    [creator]       CHAR (3) NULL,
    [modified]      DATETIME NULL,
    [modifier]      CHAR (3) NULL,
    [modifications] SMALLINT NULL,
    [UserId]        INT            NULL,
    [candidate]     CHAR (6) NOT NULL,
    [exam]          CHAR (1) NOT NULL,
    [type]          CHAR (1) NOT NULL,
    [year_start]    SMALLINT NULL,
    [year_end]      SMALLINT NULL,
    [examstaken]    TINYINT  NULL,
    [admissible]    CHAR (1) NULL,
    [id]            INT      NULL
);

