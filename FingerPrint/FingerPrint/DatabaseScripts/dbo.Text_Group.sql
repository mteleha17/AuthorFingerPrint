CREATE TABLE [dbo].[Text_Group] (
    [TextID]  INT NOT NULL,
    [GroupID] INT NOT NULL,
    PRIMARY KEY CLUSTERED (TextID, GroupID),
    FOREIGN KEY ([TextID]) REFERENCES [dbo].[Text] ([TextID])

