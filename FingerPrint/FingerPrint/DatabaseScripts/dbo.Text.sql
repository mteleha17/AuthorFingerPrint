CREATE TABLE [dbo].[Text] (
    [TextID]                INT         IDENTITY (1, 1) NOT NULL,
    [Name]                  NCHAR (100) NOT NULL,
    [Author]                NCHAR (100) NULL,
    [QuoteInd]              BIT         NOT NULL,
    [CountsWithQuotesID]    INT         NOT NULL,
    [CountsWithoutQuotesID] INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([TextID] ASC)
);

