CREATE TABLE [dbo].[abslogs] (
    [logtime]     CHAR (23)      NOT NULL,
    [absuser]     VARCHAR (30)   NOT NULL,
    [activity]    VARCHAR (60)   NULL,
    [nt_username] VARCHAR (15)   NULL,
    [suser_sname] VARCHAR (30)   NULL,
    [app_name]    VARCHAR (100)   NULL,
    [log_data]    VARCHAR (8000) NULL
);

