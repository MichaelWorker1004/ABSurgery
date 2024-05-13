CREATE TABLE [dbo].[pearson_centers] (
    [ID]      INT           NOT NULL,
    [Name]    VARCHAR (255) NULL,
    [Channel] VARCHAR (255) NULL,
    [Address] VARCHAR (255) NULL,
    [City]    VARCHAR (255) NULL,
    [State]   VARCHAR (255) NULL,
    [Country] VARCHAR (255) NULL,
    [ZipCode] VARCHAR (255) NULL,
    [Phone]   VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

