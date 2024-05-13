CREATE TABLE [dbo].[program] (
    [created]           [dbo].[created]       NOT NULL,
    [creator]           [dbo].[creator]       NOT NULL,
    [modified]          [dbo].[modified]      NULL,
    [modifier]          [dbo].[modifier]      NULL,
    [modifications]     [dbo].[modifications] NOT NULL,
    [number]            CHAR (4)              NOT NULL,
    [exam]              [dbo].[exam]          NOT NULL,
    [name1]             VARCHAR (35)          NULL,
    [name2]             VARCHAR (35)          NULL,
    [abbname]           VARCHAR (25)          NULL,
    [pd]                VARCHAR (42)          NULL,
    [last]              VARCHAR (17)          NULL,
    [l1]                SMALLINT              NULL,
    [l2]                SMALLINT              NULL,
    [l3]                SMALLINT              NULL,
    [l4]                SMALLINT              NULL,
    [l5]                SMALLINT              NULL,
    [l6]                SMALLINT              NULL,
    [total]             SMALLINT              NULL,
    [approval]          CHAR (1)              NOT NULL,
    [effdate]           SMALLDATETIME         NULL,
    [booksord]          INT                   NULL,
    [amtperbook]        MONEY                 NULL,
    [booksent]          INT                   NULL,
    [paid]              CHAR (1)              NULL,
    [invoice]           CHAR (1)              NULL,
    [sdate]             SMALLDATETIME         NULL,
    [pdate]             SMALLDATETIME         NULL,
    [pdate1]            SMALLDATETIME         NULL,
    [pdate2]            SMALLDATETIME         NULL,
    [pdate3]            SMALLDATETIME         NULL,
    [pdate4]            SMALLDATETIME         NULL,
    [pdate5]            SMALLDATETIME         NULL,
    [amt]               MONEY                 NULL,
    [amt1]              MONEY                 NULL,
    [amt2]              MONEY                 NULL,
    [amt3]              MONEY                 NULL,
    [amt4]              MONEY                 NULL,
    [amt5]              MONEY                 NULL,
    [designee]          CHAR (1)              NULL,
    [invsent]           CHAR (1)              NULL,
    [po]                VARCHAR (15)          NULL,
    [po1]               VARCHAR (10)          NULL,
    [po2]               VARCHAR (10)          NULL,
    [po3]               VARCHAR (10)          NULL,
    [po4]               VARCHAR (10)          NULL,
    [po5]               VARCHAR (10)          NULL,
    [altname]           VARCHAR (35)          NULL,
    [designeename]      VARCHAR (35)          NULL,
    [note]              VARCHAR (255)         NULL,
    [ProgramId]         INT                   IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [RRC_ID]            VARCHAR (10)          NULL,
    [RRC_TYPE]          CHAR (1)              NULL,
    [pwd]               VARCHAR (100)         NULL,
    [pwd_reset]         BIT                   NULL,
    [pwd_bak]           VARCHAR (12)          NULL,
    [crd_email]         VARCHAR (50)          NULL,
    [crd_phone]         VARCHAR (20)          NULL,
    [crd_fax]           VARCHAR (20)          NULL,
    [comments]          VARCHAR (1000)        NULL,
    [junior]            INT                   NULL,
    [senior]            INT                   CONSTRAINT [DF_program_senior] DEFAULT ((0)) NULL,
    [ship_track_id]     VARCHAR (50)          NULL,
    [rtype]             TINYINT               NULL,
    [acgme_id]          VARCHAR (10)          NULL,
    [proc_name]         VARCHAR (35)          NULL,
    [proc_email]        VARCHAR (50)          NULL,
    [timezone]          TINYINT               NULL,
    [pwd_last_modified] DATETIME              NULL,
);


GO
CREATE NONCLUSTERED INDEX [program_abbname]
    ON [dbo].[program]([abbname] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [program_number]
    ON [dbo].[program]([number] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [program_id]
    ON [dbo].[program]([ProgramId] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_program_number]
    ON [dbo].[program]([number] ASC);


GO
CREATE TRIGGER tr_program_update ON dbo.program
FOR  UPDATE 
AS

 if (select count(*) from inserted) = 0
 begin
  return
 end

 update program set program.modifier=left(APP_NAME(),3),program.modified = getdate(),program.modifications=program.modifications+1
 from program ,inserted
 where program.ProgramId = inserted.ProgramId

if @@error != 0
 begin 
	RAISERROR('Unable to update table', 16, 1)
 end

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'num residents approved per year', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'l6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'program coordinator', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'altname';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ship-to name [ite books]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'designeename';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'coordinator''s email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'crd_email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'coordinator''s phone', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'crd_phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Shipment Tracking Number, i.e. UPS Package Tracker', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'ship_track_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'coordinator''s email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'program', @level2type = N'COLUMN', @level2name = N'proc_email';

