CREATE TABLE [dbo].[Group_Group] (
    [GG_ID]    INT NOT NULL,
    [ParentID] INT NULL,
    [ChildID]  INT NULL,
    PRIMARY KEY CLUSTERED ([GG_ID] ASC),
	FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Group]([GroupID]),
	FOREIGN KEY ([ChildID]) REFERENCES [dbo].[Group]([GroupID])
);

