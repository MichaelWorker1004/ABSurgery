CREATE TABLE [dbo].[jcaho] (
    [Organization Id]         INT            NULL,
    [Program Type]            NVARCHAR (255) NULL,
    [Organization Name]       NVARCHAR (255) NULL,
    [Mailing Address]         NVARCHAR (255) NULL,
    [City ]                   NVARCHAR (255) NULL,
    [State ]                  NVARCHAR (255) NULL,
    [Zip ]                    VARCHAR (10)   NULL,
    [Phone number]            NVARCHAR (255) NULL,
    [Web Page]                NVARCHAR (255) NULL,
    [not_accredited_by_jcaho] BIT            DEFAULT ((0)) NOT NULL
);

