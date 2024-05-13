﻿CREATE TABLE [dbo].[address_hist] (
    [created]       SMALLDATETIME NOT NULL,
    [creator]       CHAR (3)      NOT NULL,
    [modified]      SMALLDATETIME NULL,
    [modifier]      CHAR (3)      NULL,
    [modifications] SMALLINT      NOT NULL,
    [type]          CHAR (1)      NOT NULL,
    [code]          CHAR (6)      NOT NULL,
    [status]        CHAR (1)      NOT NULL,
    [location]      CHAR (1)      NULL,
    [mail]          CHAR (1)      NULL,
    [title]         VARCHAR (50)  NULL,
    [institution]   VARCHAR (50)  NULL,
    [department]    VARCHAR (50)  NULL,
    [street1]       VARCHAR (100) NULL,
    [street2]       VARCHAR (100) NULL,
    [street3]       VARCHAR (50)  NULL,
    [street4]       VARCHAR (50)  NULL,
    [city]          VARCHAR (30)  NULL,
    [state]         CHAR (2)      NULL,
    [zip]           CHAR (10)     NULL,
    [note]          VARCHAR (255) NULL,
    [id]            NUMERIC (9)   NOT NULL,
    [fedex]         BIT           NULL,
    [DPV]           CHAR (1)      NULL,
    [DPVFootnote]   VARCHAR (15)  NULL,
    [County]        VARCHAR (50)  NULL,
    [RecordType]    CHAR (1)      NULL,
    [Latitude]      FLOAT (53)    NULL,
    [Longitude]     FLOAT (53)    NULL,
    [StatusNbr]     SMALLINT      NULL,
    [LastVerified]  SMALLDATETIME NULL,
    [id_hist]       NUMERIC (9)   IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_hist] ASC)
);

