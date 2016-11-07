CREATE TABLE [dbo].[Group] (
    [GroupID] INT        IDENTITY (1, 1) NOT NULL,
    [Name]    NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([GroupID] ASC),
	FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Text_Group]([GroupID])
);

