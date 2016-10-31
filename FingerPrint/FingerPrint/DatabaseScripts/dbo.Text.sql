CREATE TABLE [dbo].[Text] (
    [TextID]                INT         IDENTITY (1, 1) NOT NULL,
    [Name]                  NCHAR (100) NULL,
    [Author]                NCHAR (100) NULL,
    [QuoteInd]              BIT         NULL,
    [CountsWithQuotesID]    INT         NULL,
    [CountsWithoutQuotesID] INT         NULL,
    PRIMARY KEY CLUSTERED ([TextID] ASC)
);

