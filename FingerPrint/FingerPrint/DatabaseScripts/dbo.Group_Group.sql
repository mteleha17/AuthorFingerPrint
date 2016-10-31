CREATE TABLE [dbo].[Group_Group] (
    [GG_ID]    INT NOT NULL,
    [ParentID] INT NULL,
    [ChildID]  INT NULL,
    PRIMARY KEY CLUSTERED ([GG_ID] ASC)
);

