CREATE TABLE [dbo].[acs_opdata] (
    [auth_code]     VARCHAR (16)  NULL,
    [rrc_code]      VARCHAR (10)  NULL,
    [miscellaneous] BIT           NOT NULL,
    [hosp1_ps]      INT           NULL,
    [hosp1_ta]      INT           NULL,
    [hosp2_ps]      INT           NULL,
    [hosp2_ta]      INT           NULL,
    [created]       SMALLDATETIME CONSTRAINT [DF__acs_opdat__creat__377107A9] DEFAULT (getdate()) NOT NULL
);


GO
CREATE CLUSTERED INDEX [IX_acs_opdata]
    ON [dbo].[acs_opdata]([auth_code] ASC) WITH (FILLFACTOR = 90);

