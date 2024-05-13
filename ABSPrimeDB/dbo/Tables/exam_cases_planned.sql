CREATE TABLE [dbo].[exam_cases_planned] (
    [exam]      [dbo].[exam] NULL,
    [type]      CHAR (1)     NULL,
    [year]      SMALLINT     NULL,
    [area]      CHAR (1)     NULL,
    [day]       CHAR (1)     NULL,
    [row]       TINYINT      NULL,
    [order_num] TINYINT      NULL,
    [case_num]  VARCHAR (6)  NULL,
    [examcode]  AS           (((CONVERT([varchar],[year],(0))+[exam])+[type])+[area])
);


GO
CREATE CLUSTERED INDEX [IX_exam_cases_planned]
    ON [dbo].[exam_cases_planned]([year] ASC, [exam] ASC, [type] ASC, [area] ASC, [day] ASC, [row] ASC, [order_num] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX ['nc_examcode']
    ON [dbo].[exam_cases_planned]([examcode] ASC) WITH (FILLFACTOR = 90);

